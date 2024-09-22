using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomCategoryRepository : ICrudRepository<RoomCategory>
{
    Task<List<RoomCategory>> GetAll(int hotelId);
}
