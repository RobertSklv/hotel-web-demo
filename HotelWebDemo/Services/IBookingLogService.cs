using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface IBookingLogService
{
    Task<bool> Log(BookingEventLog log, bool throwOnError = true);

    Task<bool> Log(int bookingId, string message, bool throwOnError = true);

    Task<bool> Log(int bookingId, Func<AdminUser, string> messageCallback, bool throwOnError = true);

    BookingEventLog CreateLog(Booking? booking, string message);

    BookingEventLog CreateLog(int bookingId, string message);

    BookingEventLog CreateLog(string message);

    Task AddLog(Booking booking, BookingEventLog log);
}