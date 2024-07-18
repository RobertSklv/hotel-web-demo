using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Data.Repositories;

public interface ICustomerRepository
{
    Customer? GetFull(int id);

    Customer? Get(int id);

    Task<int> Upsert(Customer customer);

    Task<int> Save(Customer customer);

    Task<int> Delete(int id);

    Task<PaginatedList<Customer>> GetCustomers(string orderBy, bool desc, int page, int pageSize, Dictionary<string, TableFilter>? filters);

    IOrderedQueryable<Customer> OrderBy(IQueryable<Customer> customers, string propertyName, bool desc);

    CustomerAccount GetOrLoadCustomerAccount(Customer customer);

    Task<int> SaveResetPasswordToken(Customer customer, byte[] token);
}
