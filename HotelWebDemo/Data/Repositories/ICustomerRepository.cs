using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface ICustomerRepository : ICrudRepository<Customer>
{
    Customer? GetFull(int id);

    CustomerAccount GetOrLoadCustomerAccount(Customer customer);

    Task<int> SaveResetPasswordToken(Customer customer, byte[] token);
}
