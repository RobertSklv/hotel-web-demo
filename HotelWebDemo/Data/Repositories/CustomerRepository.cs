using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class CustomerRepository : CrudRepository<Customer>, ICustomerRepository
{
    public override DbSet<Customer> DbSet => db.Customers;

    public CustomerRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService, IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override IQueryable<Customer> List(DbSet<Customer> dbSet)
    {
        return dbSet
            .Include(c => c.CustomerIdentity)
            .ThenInclude(i => i.Citizenship)
            .Include(c => c.CustomerAccount)
            .ThenInclude(a => a.Address)
            .ThenInclude(a => a.Country);
    }

    public Customer? GetFull(int id)
    {
        return DbSet
            .Where(c => c.Id == id)
            .Include(c => c.CustomerIdentity)
            .ThenInclude(i => i.Citizenship)
            .Include(c => c.CustomerAccount)
            .ThenInclude(a => a.Address)
            .ThenInclude(a => a.Country)
            .FirstOrDefault();
    }

    public override Customer? Get(int id)
    {
        return DbSet
            .Where(c => c.Id == id)
            .Include(c => c.CustomerAccount)
            .FirstOrDefault();
    }

    public override async Task<int> Update(Customer entity)
    {
        DbSet.Update(entity);

        if (entity.CustomerAccount != null)
        {
            db.Entry(entity.CustomerAccount).Property(e => e.PasswordHash).IsModified = false;
            db.Entry(entity.CustomerAccount).Property(e => e.PasswordHashSalt).IsModified = false;
        }

        return await db.SaveChangesAsync();
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
