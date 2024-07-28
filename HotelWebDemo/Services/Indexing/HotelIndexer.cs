using HotelWebDemo.Data;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Services.Indexing;

public class HotelIndexer : Indexer<Hotel, HotelIndex>, IHotelIndexer
{
    protected override DbSet<Hotel> DbSet => db.Hotels;

    protected override DbSet<HotelIndex> IndexedDbSet => db.Indexed_Hotels;

    private List<Room> Rooms { get; set; }

    private List<AdminUser> AdminUsers { get; set; }

    private readonly AppDbContext db;

    public HotelIndexer(AppDbContext db)
    {
        this.db = db;
    }

    protected override void Init()
    {
        Rooms = db.Rooms.ToList();
        AdminUsers = db.AdminUsers.ToList();
    }

    protected override void LoadRelated(Hotel entity)
    {
        entity.Rooms = Rooms.Where(e => e.HotelId == entity.Id).ToList();
        entity.AdminUsers = AdminUsers.Where(e => e.HotelId == entity.Id).ToList();
    }

    protected override HotelIndex Process(Hotel entity)
    {
        return new()
        {
            Name = entity.Name,
            ShortDescription = entity.ShortDescription,
            LongDescription = entity.LongDescription,
            Stars = entity.Stars,
            RoomCount = entity.Rooms.Count,
            TotalCapacity = entity.Rooms.Sum(r => r.Capacity),
            AdminUsersCount = entity.AdminUsers.Count,
        };
    }
}
