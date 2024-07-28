using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Services;
using HotelWebDemo.Services.Indexing;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomFeatureRepository : CrudRepository<RoomFeature, RoomFeatureIndex>, IRoomFeatureRepository
{
    public override DbSet<RoomFeature> DbSet => db.RoomFeatures;

    public override DbSet<RoomFeatureIndex> IndexedDbSet => db.Indexed_RoomFeatures;

    public RoomFeatureRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IRoomFeatureIndexer? indexer)
        : base(db, filterService, sortService, indexer)
    {
    }
}
