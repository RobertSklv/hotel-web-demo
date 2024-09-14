using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IBookingTotalsService : ICrudService<BookingTotals>
{
    BookingTotals CalculateTotals(BookingViewModel viewModel);

    List<TotalsModifier> GenerateRoomTotals(BookingViewModel viewModel);

    decimal GetRoomPrice(Room room, int nights);

    decimal GetRoomFeaturesPrice(Room room, int nights);

    decimal GetTotalPriceForPeriod(Room room, int nights);

    decimal CalculatePriceOfChargeable(IChargeable chargeable, int nights);

    Task<List<TotalsModifier>> GetOrLoadModifiers(BookingTotals totals);

    Task<List<TTotalsModifier>> GetOrLoadModifiers<TTotalsModifier>(BookingTotals totals)
        where TTotalsModifier : TotalsModifier;

    Task<decimal> CalculateTotals<TTotalsModifier>(BookingTotals totals) where TTotalsModifier : TotalsModifier;

    Task<decimal> CalculateGrandTotal(BookingTotals totals);

    int OrderByTypeIndex(Type type1, Type type2, Type[] order);

    void SortTotalsModifiers(List<TotalsModifier> modifiers);
}