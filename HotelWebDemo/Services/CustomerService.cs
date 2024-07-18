using System.Security.Cryptography;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Mailing;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelWebDemo.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository repository;
    private readonly IAuthService authService;
    private readonly IMailingService mailingService;
    private readonly ILinkGeneratorSerivce linkGeneratorSerivce;

    public CustomerService(
        ICustomerRepository repository,
        IAuthService authService,
        IMailingService mailingService,
        ILinkGeneratorSerivce linkGeneratorSerivce)
    {
        this.repository = repository;
        this.authService = authService;
        this.mailingService = mailingService;
        this.linkGeneratorSerivce = linkGeneratorSerivce;
    }

    public Customer? GetFull(int id)
    {
        return repository.GetFull(id);
    }

    public Customer? Get(int id)
    {
        return repository.Get(id);
    }

    public async Task Upsert(Customer customer, ModelStateDictionary modelState)
    {
        bool newCustomer = customer.Id == 0;

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

        bool resetPasswordResult = await InitiateResetPasswordNewAccountAndNotify(customer);

        if (!resetPasswordResult)
        {
            modelState.AddModelError(string.Empty, "Something went wrong while resetting the password for the customer.");
        }
    }

    public async Task<int> Delete(int id)
    {
        return await repository.Delete(id);
    }

    public async Task<PaginatedList<Customer>> GetCustomers(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters)
    {
        bool desc = direction == "desc";

        return await repository.GetCustomers(orderBy, desc, page, pageSize, filters);
    }

    public bool CompareResetPasswordToken(Customer customer, string token)
    {
        byte[]? bytes = customer.CustomerAccount.PasswordResetToken ?? throw new Exception($"Token already used.");

        string encodedToken = UrlEncodeToken(bytes);

        return encodedToken == token;
    }

    public bool CompareResetPasswordToken(int userId, string token, ModelStateDictionary modelState)
    {
        Customer? customer = Get(userId);

        if (customer == null)
        {
            modelState.AddModelError(string.Empty, $"Customer {userId} not found!");

            return false;
        }

        return CompareResetPasswordToken(customer, token);
    }

    public Customer? CompareResetPasswordToken(ResetPasswordModel model, ModelStateDictionary modelState)
    {
        Customer? customer = Get(model.UserId);

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
        Customer? customer = Get(customerId);

        if (customer == null)
        {
            throw new Exception($"Customer not found.");
        }

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

        Customer? customer = CompareResetPasswordToken(model, modelState);

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

            int result = await repository.Save(customer);

            if (result == 0)
            {
                modelState.AddModelError(string.Empty, "An unknown error has occurred.");
            }

            return result != 0;
        }
        
        return false;
    }
}
