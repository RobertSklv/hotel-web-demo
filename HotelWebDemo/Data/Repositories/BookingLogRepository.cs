using HotelWebDemo.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class BookingLogRepository : IBookingLogRepository
{
    private readonly AppDbContext db;

    public BookingLogRepository(AppDbContext db)
	{
        this.db = db;
    }

    public async Task<int> RecordLog(BookingEventLog log)
    {
        db.Add(log);

        return await db.SaveChangesAsync();
    }

    public async Task<List<BookingEventLog>> GetOrLoadTimeline(Booking booking)
    {
        booking.BookingTimeline ??= await db.BookingEventLogs.Where(e => e.BookingId == booking.Id).ToListAsync();

        return booking.BookingTimeline;
    }
}
