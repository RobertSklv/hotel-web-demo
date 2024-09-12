using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class BookingRepository : CrudRepository<Booking, IBookingViewModel>, IBookingRepository
{
    private readonly IRoomReservationService roomReservationService;

    public override DbSet<Booking> DbSet => db.Bookings;

    public BookingRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService,
        IRoomReservationService roomReservationService)
        : base(db, filterService, sortService, searchService)
    {
        this.roomReservationService = roomReservationService;
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
            .Include(e => e.Totals)
                .ThenInclude(e => e!.Modifiers!)
            .Include(e => e.BookingTimeline!)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (booking != null && booking.ReservedRooms != null)
        {
            foreach (RoomReservation r in booking.ReservedRooms)
            {
                await roomReservationService.GetOrLoadCurrentCheckinInfo(r);
            }
        }

        return booking;
    }

    public override IQueryable<Booking> List(DbSet<Booking> dbSet)
    {
        return base.List(dbSet)
            .Include(e => e.Contact);
    }
}
