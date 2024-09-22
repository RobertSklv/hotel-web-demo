using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class BookingLogService : IBookingLogService
{
    private readonly IBookingLogRepository repository;

    public BookingLogService(IBookingLogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Log(BookingEventLog log, bool throwOnError = true)
    {
        bool success = await repository.RecordLog(log) > 0;

        if (throwOnError && !success)
        {
            throw new Exception("Failed to log booking event, an unknown error occurred");
        }

        return success;
    }

    public async Task<bool> Log(int bookingId, string message, bool throwOnError = true)
    {
        return await Log(CreateLog(bookingId, message), throwOnError);
    }

    public BookingEventLog CreateLog(Booking? booking, string message)
    {
        return new BookingEventLog
        {
            Booking = booking,
            Message = message
        };
    }

    public BookingEventLog CreateLog(int bookingId, string message)
    {
        return new BookingEventLog
        {
            BookingId = bookingId,
            Message = message
        };
    }

    public BookingEventLog CreateLog(string message)
    {
        return CreateLog(null, message);
    }

    public async Task AddLog(Booking booking, BookingEventLog log)
    {
        List<BookingEventLog> timeline = await repository.GetOrLoadTimeline(booking);
        timeline.Add(log);
    }
}
