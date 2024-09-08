using System.Security.Claims;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class AdminUserService : IAdminUserService
{
    private readonly IAdminUserRepository repository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AdminUserService(IAdminUserRepository repository, IHttpContextAccessor httpContextAccessor)
	{
        this.repository = repository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public AdminUser? Get(string username)
    {
        return repository.FindUser(username);
    }

    public AdminUser GetCurrentAdmin()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new Exception("No active HTTP context is available.");
        }

        return Get(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
            ?? throw new Exception("Failed loading current admin user.");
    }
}
