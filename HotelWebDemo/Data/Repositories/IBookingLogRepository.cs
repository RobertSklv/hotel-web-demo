using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IBookingLogRepository
{
    Task<int> RecordLog(BookingEventLog log);

    Task<List<BookingEventLog>> GetOrLoadTimeline(Booking booking);
}