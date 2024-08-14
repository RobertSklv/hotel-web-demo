using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface IBookingLogService
{
    Task<int> Log(BookingEventLog log);

    BookingEventLog CreateLog(AdminUser admin, Booking? booking, string message);

    BookingEventLog CreateLog(AdminUser admin, string message);

    Task AddLog(Booking booking, BookingEventLog log);
}