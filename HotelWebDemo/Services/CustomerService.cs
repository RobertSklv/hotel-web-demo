using System.Security.Cryptography;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Mailing;
using HotelWebDemo.Models.Utilities;

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

    public Customer? Get(int id)
    {
        return repository.Get(id);
    }

    public async Task<int> Upsert(Customer customer)
    {
        if (customer.Id == 0)
        {
            string password = Guid.NewGuid().ToString();
            byte[] passwordHashSalt = authService.GenerateSalt();
            string passwordHash = authService.Hash(password, passwordHashSalt);

            CustomerAccount account = repository.GetOrLoadCustomerAccount(customer);
            account.PasswordHashSalt = passwordHashSalt;
            account.PasswordHash = passwordHash;

            await ResetPasswordNewAccountAndNotify(customer);
        }

        return await repository.Upsert(customer);
    }

    public async Task<int> Delete(int id)
    {
        return await repository.Delete(id);
    }

    public async Task<PaginatedList<Customer>> GetCustomers(string orderBy, string direction, int page, int pageSize)
    {
        bool desc = direction == "desc";

        return await repository.GetCustomers(orderBy, desc, page, pageSize);
    }

    public byte[] GenerateResetPasswordBytes()
    {
        using RandomNumberGenerator rand = RandomNumberGenerator.Create();
        byte[] bytes = new byte[12];
        rand.GetBytes(bytes);

        return bytes;
    }

    public async Task<bool> SendResetPasswordEmail(Customer customer, string subject, string emailTemplate, byte[] token)
    {
        string bytesBase64Url = Convert.ToBase64String(token).Replace('+', '-').Replace('/', '_');
        string passwordResetUrl = linkGeneratorSerivce.GenerateResetPasswordLink(bytesBase64Url);

        ResetPasswordEmailModel model = new()
        {
            ResetPasswordUrl = passwordResetUrl,
        };

        return await mailingService.SendMail(subject, customer.CustomerAccount!.Email, emailTemplate, model);
    }

    public async Task<bool> ResetPasswordNewAccountAndNotify(Customer customer)
    {
        byte[] bytes = GenerateResetPasswordBytes();

        CustomerAccount account = repository.GetOrLoadCustomerAccount(customer);
        account.PasswordResetToken = bytes;
        account.PasswordResetStart = DateTime.UtcNow;

        return await SendResetPasswordEmail(customer, "Welcome to HotelWebDemo", "NewAccountEmail", bytes);
    }

    public async Task<bool> ResetPasswordAndNotify(Customer customer)
    {
        byte[] bytes = GenerateResetPasswordBytes();

        int result = await repository.SaveResetPasswordToken(customer, bytes);

        if (result == 0)
        {
            throw new Exception($"Failed to save the generated RP token to the database.");
        }

        return await SendResetPasswordEmail(customer, "Your password has been reset", "PasswordResetEmail", bytes);
    }
}
