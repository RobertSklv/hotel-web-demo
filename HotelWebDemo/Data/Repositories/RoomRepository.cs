using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomRepository : CrudRepository<Room>, IRoomRepository
{
    public override DbSet<Room> DbSet => db.Rooms;

    public RoomRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
        : base(db, filterService, sortService)
    {
    }
}
