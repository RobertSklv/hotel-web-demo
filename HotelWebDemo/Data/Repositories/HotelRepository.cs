using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Services;
using HotelWebDemo.Services.Indexing;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class HotelRepository : CrudRepository<Hotel, HotelIndex>, IHotelRepository
{
    public override DbSet<Hotel> DbSet => db.Hotels;

    public override DbSet<HotelIndex> IndexedDbSet => db.Indexed_Hotels;

    public HotelRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IHotelIndexer hotelIndexer)
        : base(db, filterService, sortService, hotelIndexer)
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
