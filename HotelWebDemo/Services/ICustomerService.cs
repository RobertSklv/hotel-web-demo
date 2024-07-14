using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;

namespace HotelWebDemo.Services;

public interface ICustomerService
{
    Task<PaginatedList<Customer>> GetCustomers(string orderBy, string direction, int page, int pageSize);
}