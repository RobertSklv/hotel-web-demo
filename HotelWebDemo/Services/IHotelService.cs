using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;

namespace HotelWebDemo.Services;

public interface IHotelService : ICrudService<Hotel, HotelIndex>
{
}