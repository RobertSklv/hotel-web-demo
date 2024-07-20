using HotelWebDemo.Extensions;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext db;
    private readonly IFilterService filterService;

    public CustomerRepository(AppDbContext db, IFilterService filterService)
    {
        this.db = db;
        this.filterService = filterService;
    }

    public Customer? GetFull(int id)
    {
        return db.Customers
            .Where(c => c.Id == id)
            .Include(c => c.CustomerIdentity)
            .ThenInclude(i => i.Citizenship)
            .Include(c => c.CustomerAccount)
            .ThenInclude(a => a.Address)
            .ThenInclude(a => a.Country)
            .FirstOrDefault();
    }

    public Customer? Get(int id)
    {
        return db.Customers
            .Where(c => c.Id == id)
            .Include(c => c.CustomerAccount)
            .FirstOrDefault();
    }

    public async Task<int> Upsert(Customer customer)
    {
        if (customer.Id > 0)
        {
            return await Save(customer);
        }

        await db.Customers.AddAsync(customer);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Save(Customer customer)
    {
        db.Customers.Update(customer);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        db.Customers
            .Where(c => c.Id == id)
            .Take(1)
            .ExecuteDelete();

        return await db.SaveChangesAsync();
    }

    public async Task<PaginatedList<Customer>> GetCustomers(string orderBy, bool desc, int page, int pageSize, Dictionary<string, TableFilter>? filters)
    {
        IQueryable<Customer> customers = db.Customers
            .Include(c => c.CustomerIdentity)
            .ThenInclude(i => i.Citizenship)
            .Include(c => c.CustomerAccount)
            .ThenInclude(a => a.Address)
            .ThenInclude(a => a.Country);

        customers = OrderBy(customers, orderBy, desc);
        customers = filterService.FilterBy(customers, filters);

        PaginatedList<Customer> paginatedList = await PaginatedList<Customer>.CreateAsync(customers, page, pageSize);

        return paginatedList;
    }

    public IOrderedQueryable<Customer> OrderBy(IQueryable<Customer> customers, string propertyName, bool desc)
    {
        return propertyName switch
        {
            nameof(Customer.Id) => customers.OrderByExtended(c => c.Id, desc),
            nameof(Customer.CustomerAccount_Email) => customers.OrderByExtended(c => c.CustomerAccount.Email, desc),
            nameof(Customer.FirstName) => customers.OrderByExtended(c => c.FirstName, desc),
            nameof(Customer.MiddleName) => customers.OrderByExtended(c => c.MiddleName, desc),
            nameof(Customer.LastName) => customers.OrderByExtended(c => c.LastName, desc),
            nameof(Customer.CustomerIdentity_Citizenship) => customers.OrderByExtended(c => c.CustomerIdentity.Citizenship.Name, desc),
            nameof(Customer.CustomerAccount_Address_StreetLine1) => customers.OrderByExtended(c => c.CustomerAccount.Address.StreetLine1, desc),
            nameof(Customer.CreatedAt) => customers.OrderByExtended(c => c.CreatedAt, desc),
            nameof(Customer.UpdatedAt) => customers.OrderByExtended(c => c.UpdatedAt, desc),
            _ => customers.OrderByExtended(c => c.Id, desc),
        };
    }

    public CustomerAccount GetOrLoadCustomerAccount(Customer customer)
    {
        CustomerAccount? account = customer.CustomerAccount;

        if (account == null)
        {
            account = db.CustomerAccounts.Where(a => a.Id == customer.CustomerAccountId).First();
            customer.CustomerAccount = account;
        }

        return account;
    }

    public async Task<int> SaveResetPasswordToken(Customer customer, byte[] token)
    {
        CustomerAccount account = GetOrLoadCustomerAccount(customer);

        account.PasswordResetToken = token;
        account.PasswordResetStart = DateTime.UtcNow;

        return await db.SaveChangesAsync();
    }
}
