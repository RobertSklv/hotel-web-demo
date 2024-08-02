using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class BookingService : CrudService<Booking>, IBookingService
{
    public BookingService(IBookingRepository repository)
        : base(repository)
    {
    }
}
