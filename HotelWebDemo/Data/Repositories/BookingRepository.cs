using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class BookingRepository : CrudRepository<Booking, IBookingViewModel>, IBookingRepository
{
    public override DbSet<Booking> DbSet => db.Bookings;

    public BookingRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override async Task<Booking?> Get(int id)
    {
        Booking? booking = await DbSet
            .Include(e => e.Contact)
            .Include(e => e.ReservedRooms!)
                .ThenInclude(e => e.Room)
                    .ThenInclude(e => e!.Features!)
            .Include(e => e.ReservedRooms!)
                .ThenInclude(e => e.Room)
                    .ThenInclude(e => e!.Category)
                        .ThenInclude(e => e!.Hotel)
            .Include(e => e.ReservedRooms!)
                .ThenInclude(e => e.CheckinInfo)
                    .ThenInclude(e => e!.CheckedInCustomers!)
                        .ThenInclude(e => e.Customer)
            .Include(e => e.Totals)
                .ThenInclude(e => e!.Modifiers!)
            .Include(e => e.BookingTimeline!)
            .FirstOrDefaultAsync(e => e.Id == id);

        return booking;
    }

    public override IQueryable<Booking> List(DbSet<Booking> dbSet)
    {
        return base.List(dbSet)
            .Include(e => e.Contact);
    }

    public async Task<BookingCancellation?> GetOrLoadBookingCancellation(Booking booking)
    {
        booking.BookingCancellation ??= await db.BookingCancellations.FirstOrDefaultAsync(e => e.Id == booking.BookingCancellationId);

        return booking.BookingCancellation;
    }

    public async Task<List<RoomReservation>?> GetOrLoadReservedRooms(Booking booking)
    {
        booking.ReservedRooms ??= await db.RoomReservations
            .Include(e => e.CheckinInfo)
            .Where(e => e.BookingId == booking.Id)
            .ToListAsync();

        return booking.ReservedRooms;
    }
}
