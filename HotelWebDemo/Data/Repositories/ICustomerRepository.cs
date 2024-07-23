using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;

namespace HotelWebDemo.Data.Repositories;

public interface ICustomerRepository : ICrudService<Customer>
{
    Customer? GetFull(int id);

    CustomerAccount GetOrLoadCustomerAccount(Customer customer);

    Task<int> SaveResetPasswordToken(Customer customer, byte[] token);
}
