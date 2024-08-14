using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomRepository : ICrudRepository<Room>
{
    List<RoomFeatureRoom> GetOrLoadRoomFeatureRooms(Room room);

    Task UpdateSelectedFeatures(Room room);

    Task<int> MassEnableToggle(List<int> selectedItemIds, bool enable);

    Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel);
}