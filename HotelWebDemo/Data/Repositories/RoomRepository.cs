using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomRepository : CrudRepository<Room>, IRoomRepository
{
    public override DbSet<Room> DbSet => db.Rooms;

    public RoomRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService, IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override Room? Get(int id)
    {
        Room? room = DbSet.Where(e => e.Id == id).Include(e => e.Features).FirstOrDefault();

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
        return base.List(dbSet).Include(e => e.Features);
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

    public async Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel)
    {
        return await List(
            listingModel,
            rooms => rooms
                .Include(e => e.BookingRooms!)
                .ThenInclude(e => e.Booking)
                .Where(e => 
                    e.Enabled &&
                    e.HotelId == listingModel.HotelId &&
                    !(listingModel.RoomsToReserve ?? new()).Contains(e.Id) &&
                    e.BookingRooms!.Where(e => 
                        (e.Booking!.CheckInDate >= listingModel.CheckInDate && e.Booking.CheckInDate < listingModel.CheckOutDate) ||
                        (e.Booking.CheckOutDate > listingModel.CheckInDate && e.Booking.CheckOutDate <= listingModel.CheckOutDate))
                    .Count() == 0));
    }
}
