using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext db;

    public CountryRepository(AppDbContext db)
    {
        this.db = db;
    }

    public List<Country> GetAll()
    {
        return db.Countries.ToList();
    }

    public Country Get(int id)
    {
        return db.Countries.FirstOrDefault(c => c.Id == id) ?? throw new Exception($"No such country with ID {id}.");
    }
}
