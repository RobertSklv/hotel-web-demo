using HotelWebDemo.Models.Attributes;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class RoomViewModel : Room
{
    public decimal Price
    {
        get
        {
            if (Category == null)
            {
                throw new Exception($"The category must be loaded in order to calculate the total price.");
            }

            return Category.Price;
        }
    }

    public decimal FeaturesPrice
    {
        get
        {
            if (Features == null)
            {
                throw new Exception($"The features must be loaded in order to calculate the total price.");
            }

            return Features.Sum(f => f.Price);
        }
    }

    public decimal TotalPrice => Price + FeaturesPrice;

    public decimal GetPrice(int nights) => Price * nights;

    public decimal GetFeaturesPrice(int nights)
    {
        if (Features == null)
        {
            throw new Exception($"The features must be loaded in order to calculate the total price.");
        }

        decimal total = 0;

        foreach (RoomFeature roomFeature in Features)
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

    public decimal GetTotalPriceForPeriod(int nights) => GetPrice(nights) + GetFeaturesPrice(nights);

    public static RoomViewModel ToViewModel(Room room)
    {
        return new RoomViewModel
        {
            Id = room.Id,
            Enabled = room.Enabled,
            Hotel = room.Hotel,
            HotelId = room.HotelId,
            Category = room.Category,
            CategoryId = room.CategoryId,
            Number = room.Number,
            Floor = room.Floor,
            Area = room.Area,
            Capacity = room.Capacity,
            RoomFeatureRooms = room.RoomFeatureRooms,
            Features = room.Features,
            Reservations = room.Reservations,
            CreatedAt = room.CreatedAt,
            UpdatedAt = room.UpdatedAt
        };
    }
}
