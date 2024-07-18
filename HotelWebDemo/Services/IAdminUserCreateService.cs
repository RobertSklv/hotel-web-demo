using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IAdminUserCreateService
{
    AdminUser? CreateAdminUser(AdminUserRegistration registerModel);
}