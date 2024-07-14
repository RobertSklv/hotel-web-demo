using HotelWebDemo.Models.Database;
using HotelWebDemo.Data.Repositories;

namespace StarExplorerMainServer.Areas.Admin.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository repository;

    public CountryService(ICountryRepository repository)
    {
        this.repository = repository;
    }

    public List<Country> GetAll()
    {
        return repository.GetAll();
    }

    public Country Get(int id)
    {
        return repository.Get(id);
    }
}
