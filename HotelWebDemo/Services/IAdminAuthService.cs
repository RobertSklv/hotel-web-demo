using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using System.Security.Claims;

namespace HotelWebDemo.Services;

public interface IAdminAuthService
{
    AdminUser? Authenticate(LoginCredentials loginCredentials);

    ClaimsPrincipal CreateClaimsPrincipal(AdminUser user);

    bool IsUsernameTaken(string username);

    public bool IsEmailTaken(string email);

    Task CreateDefaultAdminUser();
}