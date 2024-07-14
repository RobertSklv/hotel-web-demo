using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface ICountryRepository
{
    List<Country> GetAll();

    Country Get(int id);
}