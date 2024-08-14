using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class BookingRepository : CrudRepository<Booking, IBookingViewModel>, IBookingRepository
{
    public override DbSet<Booking> DbSet => db.Bookings;

    public BookingRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService, IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override Booking? Get(int id)
    {
        return DbSet
            .Include(e => e.Contact)
            .Include(e => e.ReservedRooms)
                .ThenInclude(e => e.Room)
                    .ThenInclude(e => e.Features)
            .Include(e => e.ReservedRooms)
                .ThenInclude(e => e.Room)
                    .ThenInclude(e => e.Category)
                        .ThenInclude(e => e.Hotel)
            .Include(e => e.Totals)
                .ThenInclude(e => e.Discounts)
            .Include(e => e.BookingTimeline)
                .ThenInclude(e => e.Admin)
            .FirstOrDefault(e => e.Id == id);
    }

    public override IQueryable<Booking> List(DbSet<Booking> dbSet)
    {
        return base.List(dbSet)
            .Include(e => e.Contact);
    }

    public async Task<List<BookingTotalsDiscount>> GetOrLoadDiscounts(BookingTotals totals)
    {
        totals.Discounts ??= await db.BookingTotalsDiscounts.Where(e => e.TotalsId == totals.Id).ToListAsync();

        return totals.Discounts;
    }
}
