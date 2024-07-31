using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface IRoomService : ICrudService<Room>
{
    Task<int> MassEnableToggle(List<int> selectedItemIds, bool enable);
}