using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IBookingRepository : ICrudRepository<Booking>
{
    Task<List<BookingTotalsDiscount>> GetOrLoadDiscounts(BookingTotals totals);
}