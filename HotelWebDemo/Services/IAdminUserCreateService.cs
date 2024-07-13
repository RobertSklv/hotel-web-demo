using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Users;

namespace HotelWebDemo.Services;

public interface IAdminUserCreateService
{
    AdminUser? CreateAdminUser(AdminUserRegistration registerModel);
}