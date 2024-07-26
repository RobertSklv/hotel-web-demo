using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class HotelRepository : CrudRepository<Hotel, HotelIndex>, IHotelRepository
{
    public override DbSet<Hotel> DbSet => db.Hotels;

    public override DbSet<HotelIndex> IndexedDbSet => db.Indexed_Hotels;

    public HotelRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
        : base(db, filterService, sortService)
    {
    }

    public Hotel? GetFull(int id)
    {
        return DbSet
            .Where(x => x.Id == id)
            .Include(e => e.Rooms)
            .Include(e => e.AdminUsers)
            .Include(e => e.Categories)
            .FirstOrDefault();
    }
}
