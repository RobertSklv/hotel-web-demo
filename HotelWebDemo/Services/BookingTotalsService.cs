using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class BookingTotalsService : IBookingTotalsService
{
    public BookingTotals CalculateTotals(BookingViewModel viewModel)
    {
        return new BookingTotals
        {
            RoomsPrice = viewModel.ReservedRooms?.Sum(r => GetRoomPrice(r, viewModel.Nights)) ?? 0,
            RoomFeaturesPrice = viewModel.ReservedRooms?.Sum(r => GetRoomFeaturesPrice(r, viewModel.Nights)) ?? 0,
            CustomGrandTotal = viewModel.HasCustomGrandTotal ? viewModel.CustomGrandTotal : null,
            //Tax = //TODO: calculate tax,
            Discounts = new() //TODO: Temporary. Here it needs to be checked whether there are some active discounts that affect the selected items and load them up.
        };
    }

    public decimal GetRoomPrice(Room room, int nights)
    {
        if (room.Category == null)
        {
            throw new Exception($"The category must be loaded in order to calculate the total price.");
        }

        return room.Category.Price * nights;
    }

    public decimal GetRoomFeaturesPrice(Room room, int nights)
    {
        if (room.Features == null)
        {
            throw new Exception($"The features must be loaded in order to calculate the total price.");
        }

        decimal total = 0;

        foreach (RoomFeature roomFeature in room.Features)
        {
            decimal featurePrice = roomFeature.Price;

            if (roomFeature.IsPricePerNight)
            {
                featurePrice *= nights;
            }

            total += featurePrice;
        }

        return total;
    }

    public decimal GetTotalPriceForPeriod(Room room, int nights)
    {
        return GetRoomPrice(room, nights) + GetRoomFeaturesPrice(room, nights);
    }
}
