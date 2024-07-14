using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;

namespace HotelWebDemo.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository repository;

    public CustomerService(ICustomerRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<Customer>> GetCustomers(string orderBy, string direction, int page, int pageSize)
    {
        bool desc = direction == "desc";

        return await repository.GetCustomers(orderBy, desc, page, pageSize);
    }
}
