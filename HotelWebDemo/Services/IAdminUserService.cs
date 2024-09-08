using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface IAdminUserService
{
    AdminUser? Get(string username);

    AdminUser GetCurrentAdmin();
}