using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomRepository : ICrudRepository<Room>
{
    List<RoomFeatureRoom> GetOrLoadRoomFeatureRooms(Room room);

    Task UpdateSelectedFeatures(Room room);

    Task<int> MassEnableToggle(List<int> selectedItemIds, bool enable);

    IQueryable<Room> AddEnabledFilter(IQueryable<Room> queryable, bool enabled);

    IQueryable<Room> AddHotelIdFilter(IQueryable<Room> queryable, int hotelId);

    IQueryable<Room> AddRoomIdFilter(IQueryable<Room> queryable, List<int> ids, bool includes);

    IQueryable<Room> AddRoomCategoryIdFilter(IQueryable<Room> queryable, List<int> ids, bool includes);
}