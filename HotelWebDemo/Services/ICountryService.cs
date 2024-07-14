using HotelWebDemo.Models.Database;

namespace StarExplorerMainServer.Areas.Admin.Services;

public interface ICountryService
{
    List<Country> GetAll();

    Country Get(int id);
}