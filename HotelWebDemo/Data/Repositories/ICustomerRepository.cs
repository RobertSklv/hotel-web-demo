using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface ICustomerRepository : ICrudRepository<Customer>
{
    Task<Customer?> GetByNationalId(string nationalId);

    Task<Customer?> GetByPassportId(string passportId);

    CustomerAccount GetOrLoadCustomerAccount(Customer customer);

    Task<int> SaveResetPasswordToken(Customer customer, byte[] token);
}
