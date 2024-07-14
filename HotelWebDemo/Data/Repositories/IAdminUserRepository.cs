using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IAdminUserRepository
{
    Task<int> SaveUser(AdminUser user);

    AdminUser? FindUser(string usernameOrEmail);

    bool IsUsernameTaken(string username);

    bool IsEmailTaken(string email);

    int GetAdminUserCount();

    AdminRole? FindRole(string code);
}
