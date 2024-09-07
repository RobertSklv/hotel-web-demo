using HotelWebDemo.Models.Database;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Services;

namespace StarExplorerMainServer.Areas.Admin.Services;

public class CountryService : CrudService<Country>, ICountryService
{
    public CountryService(ICountryRepository repository)
        : base(repository)
    {
    }
}
