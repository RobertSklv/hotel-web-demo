using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;

namespace HotelWebDemo.Data.Repositories;

public interface ICustomerRepository
{
    Task<PaginatedList<Customer>> GetCustomers(string orderBy, bool desc, int page, int pageSize);

    IOrderedQueryable<Customer> OrderBy(IQueryable<Customer> customers, string propertyName, bool desc);
}
