﻿using System.Security.Cryptography;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Mailing;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Services;

public class CustomerService : CrudService<Customer, CustomerViewModel>, ICustomerService
{
    private readonly ICustomerRepository repository;
    private readonly ICountryService countryService;
    private readonly IAuthService authService;
    private readonly IMailingService mailingService;
    private readonly ILinkGeneratorSerivce linkGeneratorSerivce;

    public CustomerService(
        ICustomerRepository repository,
        ICountryService countryService,
        IAuthService authService,
        IMailingService mailingService,
        ILinkGeneratorSerivce linkGeneratorSerivce)
        : base(repository)
    {
        this.repository = repository;
        this.countryService = countryService;
        this.authService = authService;
        this.mailingService = mailingService;
        this.linkGeneratorSerivce = linkGeneratorSerivce;
    }

    public async Task Upsert(CustomerViewModel customerViewModel, ModelStateDictionary modelState)
    {
        bool newCustomer = customerViewModel.Id == 0;
        Customer customer = await ViewModelToEntityAsync(customerViewModel);

        if (newCustomer)
        {
            string password = Guid.NewGuid().ToString();
            byte[] passwordHashSalt = authService.GenerateSalt();
            string passwordHash = authService.Hash(password, passwordHashSalt);

            CustomerAccount account = repository.GetOrLoadCustomerAccount(customer);
            account.PasswordHashSalt = passwordHashSalt;
            account.PasswordHash = passwordHash;
        }

        int upsertResult = await repository.Upsert(customer);

        if (upsertResult == 0)
        {
            modelState.AddModelError(string.Empty, "Something went wrong while saving the customer");
        }

        if (newCustomer)
        {
            bool resetPasswordResult = await InitiateResetPasswordNewAccountAndNotify(customer);

            if (!resetPasswordResult)
            {
                modelState.AddModelError(string.Empty, "Something went wrong while resetting the password for the customer.");
            }
        }
    }

    public override Customer ViewModelToEntity(CustomerViewModel viewModel)
    {
        return new Customer()
        {
            Id = viewModel.Id,
            FirstName = viewModel.FirstName,
            MiddleName = viewModel.MiddleName,
            LastName = viewModel.LastName,
            PassportId = viewModel.PassportId,
            NationalId = viewModel.NationalId,
            Citizenship = viewModel.Citizenship,
            CitizenshipId = viewModel.CitizenshipId,
            DateOfBirth = viewModel.DateOfBirth,
            Gender = viewModel.Gender,
            Address = new Address()
            {
                StreetLine1 = viewModel.StreetLine1,
                StreetLine2 = viewModel.StreetLine2,
                StreetLine3 = viewModel.StreetLine3,
                Country = viewModel.Country,
                CountryId = viewModel.CountryId,
                City = viewModel.City,
                PostalCode = viewModel.PostalCode,
                Phone = viewModel.Phone,
            },
            CustomerAccount = new CustomerAccount()
            {
                Email = viewModel.Email
            }
        };
    }

    public override CustomerViewModel EntityToViewModel(Customer customer)
    {
        if (customer.CustomerAccount == null)
        {
            throw new Exception("Customer account not loaded.");
        }

        if (customer.Address == null)
        {
            throw new Exception("Customer address not loaded");
        }

        return new CustomerViewModel()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            MiddleName = customer.MiddleName,
            LastName = customer.LastName,
            Email = customer.CustomerAccount.Email,
            DateOfBirth = customer.DateOfBirth,
            PassportId = customer.PassportId,
            NationalId = customer.NationalId,
            Citizenship = customer.Citizenship,
            CitizenshipId = customer.CitizenshipId,
            StreetLine1 = customer.Address.StreetLine1,
            StreetLine2 = customer.Address.StreetLine2,
            StreetLine3 = customer.Address.StreetLine3,
            Country = customer.Address.Country,
            CountryId = customer.Address.CountryId,
            City = customer.Address.City,
            PostalCode = customer.Address.PostalCode,
            Phone = customer.Address.Phone,
        };
    }

    public override async Task<Table<Customer>> CreateListingTable(ListingModel<Customer> listingModel, PaginatedList<Customer> items)
    {
        List<Country> countries = await countryService.GetAll();

        return (await base.CreateListingTable(listingModel, items))
            .SetAdjustablePageSize(true)
            .OverrideColumnName(nameof(Customer.CreatedAt), "Registration date")
            .SetSelectableOptionsSource(nameof(Customer.Citizenship), countries);
    }

    public override Table<Customer> CreateDeleteRowAction(Table<Customer> table)
    {
        return table;
    }

    public bool CompareResetPasswordToken(Customer customer, string token)
    {
        byte[]? bytes = customer.CustomerAccount.PasswordResetToken ?? throw new Exception($"Token already used.");

        string encodedToken = UrlEncodeToken(bytes);

        return encodedToken == token;
    }

    public async Task<bool> CompareResetPasswordToken(int userId, string token, ModelStateDictionary modelState)
    {
        Customer? customer = await Get(userId);

        if (customer == null)
        {
            modelState.AddModelError(string.Empty, $"Customer {userId} not found!");

            return false;
        }

        return CompareResetPasswordToken(customer, token);
    }

    public async Task<Customer?> CompareResetPasswordToken(ResetPasswordModel model, ModelStateDictionary modelState)
    {
        Customer? customer = await Get(model.UserId);

        if (customer == null)
        {
            modelState.AddModelError(string.Empty, $"Customer {model.UserId} not found!");

            return null;
        }

        return CompareResetPasswordToken(customer, model.Token) ? customer : null;
    }

    public byte[] GenerateResetPasswordBytes()
    {
        using RandomNumberGenerator rand = RandomNumberGenerator.Create();
        byte[] bytes = new byte[12];
        rand.GetBytes(bytes);

        return bytes;
    }

    public string UrlEncodeToken(byte[] token)
    {
        return Convert.ToBase64String(token).Replace('+', '-').Replace('/', '_');
    }

    public async Task<bool> SendResetPasswordEmail(Customer customer, string subject, string emailTemplate, byte[] token)
    {
        string passwordResetUrl = linkGeneratorSerivce.GenerateResetPasswordLink(customer.Id, UrlEncodeToken(token));

        ResetPasswordEmailModel model = new()
        {
            ResetPasswordUrl = passwordResetUrl,
        };

        return await mailingService.SendMail(subject, customer.CustomerAccount!.Email, emailTemplate, model);
    }

    public async Task<bool> InitiateResetPasswordNewAccountAndNotify(Customer customer)
    {
        byte[] bytes = GenerateResetPasswordBytes();

        CustomerAccount account = repository.GetOrLoadCustomerAccount(customer);
        account.PasswordResetToken = bytes;
        account.PasswordResetStart = DateTime.UtcNow;

        return await SendResetPasswordEmail(customer, "Welcome to HotelWebDemo", "NewAccountEmail", bytes);
    }

    public async Task<bool> InitiateResetPasswordAndNotify(int customerId)
    {
        Customer? customer = await Get(customerId) ?? throw new Exception($"Customer not found.");

        byte[] bytes = GenerateResetPasswordBytes();

        int result = await repository.SaveResetPasswordToken(customer, bytes);

        if (result == 0)
        {
            throw new Exception($"Failed to save the generated RP token to the database.");
        }

        return await SendResetPasswordEmail(customer, "Password reset request", "PasswordResetEmail", bytes);
    }

    public async Task<bool> ResetPassword(ResetPasswordModel model, ModelStateDictionary modelState)
    {
        const int passwordResetLinkDurationHours = 3;

        bool isValid = true;

        Customer? customer = await CompareResetPasswordToken(model, modelState);

        if (customer == null)
        {
            modelState.AddModelError(string.Empty, "User not found.");
            isValid = false;
        }
        else if (customer.CustomerAccount?.PasswordResetStart?.AddHours(passwordResetLinkDurationHours) < DateTime.UtcNow)
        {
            modelState.AddModelError(string.Empty, "The token is expired.");
            isValid = false;
        }

        if (isValid)
        {
            byte[] passwordHashSalt = authService.GenerateSalt();
            string passwordHash = authService.Hash(model.Password, passwordHashSalt);

            customer!.CustomerAccount.PasswordHash = passwordHash;
            customer!.CustomerAccount.PasswordHashSalt = passwordHashSalt;
            customer!.CustomerAccount.PasswordResetToken = null;
            customer!.CustomerAccount.PasswordResetStart = null;

            int result = await repository.Update(customer);

            if (result == 0)
            {
                modelState.AddModelError(string.Empty, "An unknown error has occurred.");
            }

            return result != 0;
        }
        
        return false;
    }

    public async Task<Customer?> GetByNationalId(string nationalId)
    {
        if (string.IsNullOrEmpty(nationalId))
        {
            return null;
        }

        return await repository.GetByNationalId(nationalId);
    }

    public async Task<Customer?> GetByPassportId(string passportId)
    {
        if (string.IsNullOrEmpty(passportId))
        {
            return null;
        }

        return await repository.GetByPassportId(passportId);
    }
}
