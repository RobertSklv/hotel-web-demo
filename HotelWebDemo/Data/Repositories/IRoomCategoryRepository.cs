using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomCategoryRepository : ICrudRepository<RoomCategory>
{
    List<RoomCategory> GetAll(int hotelId);
}
