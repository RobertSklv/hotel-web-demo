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

    public async Task<int> Log(BookingEventLog log)
    {
        return await repository.RecordLog(log);
    }

    public BookingEventLog CreateLog(AdminUser admin, Booking? booking, string message)
    {
        return new BookingEventLog
        {
            Admin = admin,
            Booking = booking,
            Message = message
        };
    }

    public BookingEventLog CreateLog(AdminUser admin, string message)
    {
        return CreateLog(admin, null, message);
    }

    public async Task AddLog(Booking booking, BookingEventLog log)
    {
        List<BookingEventLog> timeline = await repository.GetOrLoadTimeline(booking);
        timeline.Add(log);
    }
}
