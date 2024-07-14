﻿using HotelWebDemo.Extensions;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext db;

    public CustomerRepository(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<PaginatedList<Customer>> GetCustomers(string orderBy, bool desc, int page, int pageSize)
    {
        IQueryable<Customer> customers = db.Customers
            .Include(c => c.CustomerIdentity)
            .Include(c => c.CustomerAccount);

        customers = OrderBy(customers, orderBy, desc);

        PaginatedList<Customer> paginatedList = await PaginatedList<Customer>.CreateAsync(customers, page, pageSize);

        return paginatedList;
    }

    public IOrderedQueryable<Customer> OrderBy(IQueryable<Customer> customers, string propertyName, bool desc)
    {
        return propertyName switch
        {
            nameof(Customer.Id) => customers.OrderByExtended(c => c.Id, desc),
            nameof(Customer.FirstName) => customers.OrderByExtended(c => c.FirstName, desc),
            nameof(Customer.MiddleName) => customers.OrderByExtended(c => c.MiddleName, desc),
            nameof(Customer.LastName) => customers.OrderByExtended(c => c.LastName, desc),
            nameof(Customer.CreatedAt) => customers.OrderByExtended(c => c.CreatedAt, desc),
            nameof(Customer.UpdatedAt) => customers.OrderByExtended(c => c.UpdatedAt, desc),
            _ => customers.OrderByExtended(c => c.Id, desc),
        };
    }
}
