using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Users;

namespace HotelWebDemo.Services;

public class AdminUserCreateService : IAdminUserCreateService
{
    private readonly IAuthService authService;

    public AdminUserCreateService(IAuthService authService)
    {
        this.authService = authService;
    }

    public AdminUser? CreateAdminUser(AdminUserRegistration registerModel)
    {
        byte[] passwordHashSalt = authService.GenerateSalt();
        string passwordHash = authService.Hash(registerModel.Password, passwordHashSalt);

        AdminUser user = new()
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            PasswordHash = passwordHash,
            PasswordHashSalt = passwordHashSalt
        };

        return user;
    }
}
