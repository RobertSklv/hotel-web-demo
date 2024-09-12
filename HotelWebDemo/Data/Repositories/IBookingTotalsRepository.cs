using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IBookingTotalsRepository : ICrudRepository<BookingTotals>
{
    Task<List<TotalsModifier>> GetOrLoadModifiers(BookingTotals totals);

    Task<List<TTotalsModifier>> GetOrLoadModifiers<TTotalsModifier>(BookingTotals totals) where TTotalsModifier : TotalsModifier;
}