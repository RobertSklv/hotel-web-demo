using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IBookingTotalsService
{
    BookingTotals CalculateTotals(BookingViewModel viewModel);

    decimal GetRoomPrice(Room room, int nights);

    decimal GetRoomFeaturesPrice(Room room, int nights);

    decimal GetTotalPriceForPeriod(Room room, int nights);
}