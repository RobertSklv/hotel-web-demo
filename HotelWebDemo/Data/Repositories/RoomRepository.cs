using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomRepository : CrudRepository<Room>, IRoomRepository
{
    public override DbSet<Room> DbSet => db.Rooms;

    public RoomRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override async Task<Room?> Get(int id)
    {
        Room? room = await DbSet
            .Include(e => e.Features)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (room != null)
        {
            room.SelectedFeatureIds = room.Features?.ConvertAll(f => f.Id) ?? new();
        }

        return room;
    }

    public override async Task<List<Room>> GetByIds(IEnumerable<int> ids)
    {
        return await DbSet
            .Include(e => e.Category)
            .Include(e => e.Features)
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();
    }

    public override IQueryable<Room> List(DbSet<Room> dbSet)
    {
        return base.List(dbSet)
            .Include(e => e.Category)
            .Include(e => e.Features);
    }

    public override async Task<int> Update(Room entity)
    {
        await UpdateSelectedFeatures(entity);

        return await base.Update(entity);
    }

    public List<RoomFeatureRoom> GetOrLoadRoomFeatureRooms(Room room)
    {
        room.RoomFeatureRooms ??= db.RoomFeatureRooms.Where(e => e.RoomId == room.Id).ToList();

        return room.RoomFeatureRooms;
    }

    public async Task<List<RoomReservation>> GetOrLoadRoomReservations(Room room)
    {
        room.Reservations ??= await db.RoomReservations
            .Where(e => e.RoomId == room.Id)
            .ToListAsync();

        return room.Reservations;
    }

    public async Task UpdateSelectedFeatures(Room room)
    {
        GetOrLoadRoomFeatureRooms(room);

        //Remove missing
        await db.RoomFeatureRooms.Where(e => e.RoomId == room.Id && !room.SelectedFeatureIds.Contains(e.RoomFeatureId)).ExecuteDeleteAsync();

        //Insert new ones
        foreach (int featureId in room.SelectedFeatureIds)
        {
            if (!room.RoomFeatureRooms.Any(e => e.RoomFeatureId == featureId))
            {
                RoomFeatureRoom @new = new()
                {
                    Room = room,
                    RoomId = room.Id,
                    RoomFeatureId = featureId
                };
                room.RoomFeatureRooms.Add(@new);
            }
        }
    }

    public async Task<int> MassEnableToggle(List<int> selectedItemIds, bool enable)
    {
        return await db.Rooms
            .Where(e => selectedItemIds.Contains(e.Id))
            .ExecuteUpdateAsync(e => e.SetProperty(e => e.Enabled, enable));
    }

    public IQueryable<Room> AddEnabledFilter(IQueryable<Room> queryable, bool enabled)
    {
        return queryable.Where(e => e.Enabled == enabled);
    }

    public IQueryable<Room> AddHotelIdFilter(IQueryable<Room> queryable, int hotelId)
    {
        return queryable.Where(e => e.HotelId == hotelId);
    }

    public IQueryable<Room> AddRoomIdFilter(IQueryable<Room> queryable, List<int> ids, bool includes)
    {
        return queryable.Where(e => ids.Contains(e.Id) == includes);
    }

    public IQueryable<Room> AddRoomCategoryIdFilter(IQueryable<Room> queryable, List<int> ids, bool includes)
    {
        return queryable.Where(e => ids.Contains(e.CategoryId) == includes);
    }
}
