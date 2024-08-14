using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class AdminUserService : IAdminUserService
{
    private readonly IAdminUserRepository repository;

    public AdminUserService(IAdminUserRepository repository)
	{
        this.repository = repository;
    }

    public AdminUser? Get(string username)
    {
        return repository.FindUser(username);
    }
}
