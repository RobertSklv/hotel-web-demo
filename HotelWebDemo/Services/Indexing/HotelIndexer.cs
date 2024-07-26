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

    public HotelIndexer(AppDbContext db) : base(db)
    {
    }

    protected override void Init()
    {
        Rooms = db.Rooms.ToList();
        AdminUsers = db.AdminUsers.ToList();
    }

    protected override HotelIndex Process(Hotel entity)
    {
        List<Room> rooms = Rooms.Where(e => e.HotelId == entity.Id).ToList();
        List<AdminUser> adminUsers = AdminUsers.Where(e => e.HotelId == entity.Id).ToList();

        return new()
        {
            Name = entity.Name,
            ShortDescription = entity.ShortDescription,
            LongDescription = entity.LongDescription,
            Stars = entity.Stars,
            RoomCount = rooms.Count,
            TotalCapacity = rooms.Sum(r => r.Capacity),
            AdminUsersCount = adminUsers.Count,
        };
    }
}
