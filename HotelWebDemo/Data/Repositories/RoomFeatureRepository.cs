using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomFeatureRepository : CrudRepository<RoomFeature>, IRoomFeatureRepository
{
    public override DbSet<RoomFeature> DbSet => db.RoomFeatures;

    public RoomFeatureRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override IQueryable<RoomFeature> List(DbSet<RoomFeature> dbSet)
    {
        return base.List(dbSet)
            .Include(e => e.Hotel)
            .Include(e => e.Rooms);
    }
}
