using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("Rooms")]
public class Room : BaseEntity
{
    [TableColumn]
    public bool Enabled { get; set; } = true;

    [TableColumn]
    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }

    [TableColumn]
    public RoomCategory? Category { get; set; }

    public int CategoryId { get; set; }

    [TableColumn]
    public int Number { get; set; }

    [TableColumn]
    public int Floor { get; set; }

    [TableColumn(SpecialFormat = TableColumnSpecialFormat.MeterSquared)]
    public float Area { get; set; }

    [Range(0, 10)]
    [TableColumn]
    public int Capacity { get; set; }

    [JsonIgnore]
    public List<RoomFeatureRoom>? RoomFeatureRooms { get; set; }

    public List<RoomFeature>? Features { get; set; }

    [JsonIgnore]
    public List<RoomReservation>? BookingRooms { get; set; }

    [NotMapped]
    public List<int> SelectedFeatureIds { get; set; } = new();

    [TableColumn(Name = "Premium features", Orderable = false, Filterable = false)]
    public string FeaturesEnumerated => Features != null && Features.Count > 0
        ? string.Join(", ", Features.ConvertAll(f => $"{f.Name} (${f.Price:#0.00})"))
        : "None";

    public string CategoryNameAndCapacity
    {
        get
        {
            if (Category == null)
            {
                throw new Exception($"The category is not loaded.");
            }

            string adultsLabel = Capacity > 1 ? "adults" : "adult";

            return $"{Category.Name} ({Capacity} {adultsLabel})";
        }
    }

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

            decimal total = 0;

            foreach (RoomFeature roomFeature in Features)
            {
                total += roomFeature.Price;
            }

            return total;
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

    //[NotMapped]
    //public Booking? ActiveBooking => BookingRooms?.Where(e => e.Booking?.ExpirationDate > DateTime.UtcNow).FirstOrDefault()?.Booking;
}
