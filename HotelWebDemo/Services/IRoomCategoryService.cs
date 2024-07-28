using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Json;

namespace HotelWebDemo.Services;

public interface IRoomCategoryService : ICrudService<RoomCategory>
{
    List<RoomCategory> GetAll(int hotelId);

    List<RoomCategoryOption> GetAllAsOptions(int hotelId);
}
