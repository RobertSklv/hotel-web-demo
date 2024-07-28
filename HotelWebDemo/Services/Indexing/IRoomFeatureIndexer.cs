using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;

namespace HotelWebDemo.Services.Indexing;

public interface IRoomFeatureIndexer : IIndexer<RoomFeature, RoomFeatureIndex>
{
}