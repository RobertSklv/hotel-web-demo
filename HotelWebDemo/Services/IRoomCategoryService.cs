using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Json;

namespace HotelWebDemo.Services;

public interface IRoomCategoryService : ICrudService<RoomCategory>
{
    Task<List<RoomCategory>> GetAll(int hotelId);

    Task<List<RoomCategoryOption>> GetAllAsOptions(int hotelId);
}
