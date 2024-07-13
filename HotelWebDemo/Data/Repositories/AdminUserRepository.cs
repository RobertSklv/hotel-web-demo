﻿using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public class AdminUserRepository : IAdminUserRepository
{
    private readonly AppDbContext db;

    public AdminUserRepository(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<int> SaveUser(AdminUser user)
    {
        db.AdminUsers.Add(user);

        return await db.SaveChangesAsync();
    }

    public AdminUser? FindUser(string usernameOrEmail)
    {
        return db.AdminUsers.Where(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail).FirstOrDefault();
    }

    public bool IsUsernameTaken(string username)
    {
        return db.AdminUsers.Any(u => u.UserName == username);
    }

    public bool IsEmailTaken(string email)
    {
        return db.AdminUsers.Any(u => u.Email == email);
    }

    public int GetAdminUserCount()
    {
        return db.AdminUsers.Count();
    }
}