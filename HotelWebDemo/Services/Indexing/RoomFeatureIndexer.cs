using HotelWebDemo.Data;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Services.Indexing;

public class RoomFeatureIndexer : Indexer<RoomFeature, RoomFeatureIndex>, IRoomFeatureIndexer
{
    protected override DbSet<RoomFeature> DbSet => db.RoomFeatures;

    protected override DbSet<RoomFeatureIndex> IndexedDbSet => db.Indexed_RoomFeatures;

    private List<RoomFeatureRoom> RoomFeatureRooms { get; set; }

    private List<BookingItemRoomFeature> BookingItemRoomFeatures { get; set; }

    private readonly AppDbContext db;

    public RoomFeatureIndexer(AppDbContext db)
    {
        this.db = db;
    }

    protected override void Init()
    {
        RoomFeatureRooms = db.RoomFeatureRooms.ToList();
        BookingItemRoomFeatures = db.BookingItemRoomFeatures.ToList();
    }

    protected override void LoadRelated(RoomFeature entity)
    {
        entity.RoomFeatureRooms = RoomFeatureRooms.Where(e => e.RoomFeatureId == entity.Id).ToList();
        entity.BookedFeatures = BookingItemRoomFeatures.Where(e => e.RoomFeatureId == entity.Id).ToList();
    }

    protected override RoomFeatureIndex Process(RoomFeature entity)
    {
        return new()
        {
            Code = entity.Code,
            Name = entity.Name,
            Price = entity.Price,
            Hotel = entity.Hotel,
            HotelId = entity.HotelId,
            TimesUsed = entity.RoomFeatureRooms.Count,
            TimesBooked = entity.BookedFeatures.Count,
        };
    }
}
