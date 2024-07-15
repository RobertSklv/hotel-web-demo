using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;

namespace HotelWebDemo.Data.Repositories;

public interface ICustomerRepository
{
    Customer? Get(int id);

    Task<int> Upsert(Customer customer);

    Task<int> Delete(int id);

    Task<PaginatedList<Customer>> GetCustomers(string orderBy, bool desc, int page, int pageSize);

    IOrderedQueryable<Customer> OrderBy(IQueryable<Customer> customers, string propertyName, bool desc);

    CustomerAccount GetOrLoadCustomerAccount(Customer customer);

    Task<int> SaveResetPasswordToken(Customer customer, byte[] token);
}
