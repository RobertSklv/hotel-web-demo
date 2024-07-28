using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Services;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomFeatureRepository : ICrudRepository<RoomFeature, RoomFeatureIndex>
{
}
