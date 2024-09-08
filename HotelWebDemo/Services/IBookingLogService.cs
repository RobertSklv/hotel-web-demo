using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface IBookingLogService
{
    Task<int> Log(BookingEventLog log);

    BookingEventLog CreateLog(Booking? booking, string message);

    BookingEventLog CreateLog(int bookingId, string message);

    BookingEventLog CreateLog(string message);

    Task AddLog(Booking booking, BookingEventLog log);
}