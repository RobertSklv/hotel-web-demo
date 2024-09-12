using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class BookingTotalsRepository : CrudRepository<BookingTotals>, IBookingTotalsRepository
{
    public override DbSet<BookingTotals> DbSet => db.BookingTotals;

    public BookingTotalsRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public async Task<List<TotalsModifier>> GetOrLoadModifiers(BookingTotals totals)
    {
        totals.Modifiers ??= await db.TotalsModifiers.Where(e => e.TotalsId == totals.Id).ToListAsync();

        return totals.Modifiers;
    }

    public async Task<List<TTotalsModifier>> GetOrLoadModifiers<TTotalsModifier>(BookingTotals totals)
        where TTotalsModifier : TotalsModifier
    {
        List<TTotalsModifier> modifiers = new();

        foreach (TotalsModifier m in await GetOrLoadModifiers(totals))
        {
            if (m is TTotalsModifier tm)
            {
                modifiers.Add(tm);
            }
        }

        return modifiers;
    }
}
