﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class CustomerRepository : CrudRepository<Customer>, ICustomerRepository
{
    public override DbSet<Customer> DbSet => db.Customers;

    public override DbSet<Customer> IndexedDbSet => db.Customers;

    public CustomerRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
        : base(db, filterService, sortService)
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
