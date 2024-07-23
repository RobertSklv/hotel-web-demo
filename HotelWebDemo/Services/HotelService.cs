using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class HotelService : CrudService<Hotel>, IHotelService
{
    public HotelService(IHotelRepository repository)
        : base(repository)
    {
    }
}
