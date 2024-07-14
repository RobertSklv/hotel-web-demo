using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Users;
using System.Security.Claims;

namespace HotelWebDemo.Services;

public class AdminAuthService : IAdminAuthService
{
    private readonly IAdminUserRepository adminUserRepository;
    private readonly IAuthService authService;
    private readonly IAdminUserCreateService adminUserCreateService;
    private readonly IConfiguration config;

    public AdminAuthService(
        IAdminUserRepository adminUserRepository,
        IAuthService authService,
        IAdminUserCreateService adminUserCreateService,
        IConfiguration config)
    {
        this.adminUserRepository = adminUserRepository;
        this.authService = authService;
        this.adminUserCreateService = adminUserCreateService;
        this.config = config;
    }

    public AdminUser? Authenticate(LoginCredentials loginCredentials)
    {
        AdminUser? user = adminUserRepository.FindUser(loginCredentials.UserNameOrEmail);

        if (user != null)
        {
            if (authService.CompareHashes(loginCredentials.Password, user.PasswordHash, user.PasswordHashSalt))
            {
                return user;
            }
        }

        return null;
    }

    public ClaimsPrincipal CreateClaimsPrincipal(AdminUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Code),
        };
        ClaimsIdentity identity = new(claims, "AdminCookie");
        ClaimsPrincipal principal = new(identity);

        return principal;
    }

    public bool IsUsernameTaken(string username)
    {
        return adminUserRepository.IsUsernameTaken(username);
    }

    public bool IsEmailTaken(string email)
    {
        return adminUserRepository.IsEmailTaken(email);
    }

    public async Task CreateDefaultAdminUser()
    {
        int count = adminUserRepository.GetAdminUserCount();

        if (count == 0)
        {
            string? username = config["Admin:InitialUser:Name"] ?? null;
            string? email = config["Admin:InitialUser:Email"] ?? null;
            string? password = config["Admin:InitialUser:Password"] ?? null;

            if (username == null || email == null || password == null)
            {
                return;
            }

            if (IsUsernameTaken(username))
            {
                return;
            }

            AdminRole? role = adminUserRepository.FindRole("Administrator");

            if (role == null)
            {
                return;
            }

            AdminUserRegistration registerModel = new()
            {
                UserName = username,
                Email = email,
                Password = password,
                RoleId = role.Id,
            };

            AdminUser? admin = adminUserCreateService.CreateAdminUser(registerModel);

            if (admin == null)
            {
                return;
            }

            await adminUserRepository.SaveUser(admin);
        }
    }
}
