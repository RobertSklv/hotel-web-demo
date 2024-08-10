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
}
