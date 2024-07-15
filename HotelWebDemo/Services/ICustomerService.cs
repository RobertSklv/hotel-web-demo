using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;

namespace HotelWebDemo.Services;

public interface ICustomerService
{
    Customer? Get(int id);

    Task<int> Upsert(Customer customer);

    Task<int> Delete(int id);

    Task<PaginatedList<Customer>> GetCustomers(string orderBy, string direction, int page, int pageSize);

    byte[] GenerateResetPasswordBytes();

    Task<bool> SendResetPasswordEmail(Customer customer, string subject, string emailTemplate, byte[] token);

    Task<bool> ResetPasswordNewAccountAndNotify(Customer customer);

    Task<bool> ResetPasswordAndNotify(Customer customer);
}