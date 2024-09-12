using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("RoomFeatures")]
[SelectOption]
public class RoomFeature : BaseEntity, IChargeable
{
    [StringLength(32, MinimumLength = 1)]
    [Column(TypeName = "varchar")]
    [RegularExpression("^(?:[A-Za-z][A-Za-z0-9]*(?:_[A-Za-z][A-Za-z0-9]*)*)$", ErrorMessage = "Invalid code.")]
    [TableColumn]
    public string Code { get; set; }

    [StringLength(64, MinimumLength = 1)]
    [TableColumn]
    public string Name { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [TableColumn]
    [Display(Name = "Price per night")]
    public bool IsPricePerNight { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [TableColumn]
    public Hotel Hotel { get; set; }

    public int HotelId { get; set; }

    [JsonIgnore]
    public List<Room>? Rooms { get; set; }

    [JsonIgnore]
    public List<RoomFeatureRoom>? RoomFeatureRooms { get; set; }

    [JsonIgnore]
    public List<BookingItemRoomFeature>? BookedFeatures { get; set; }

    [TableColumn(Name = "Times used")]
    public int TimesUsed => Rooms?.Count ?? 0;

    [TableColumn(Name = "Times booked")]
    public int TimesBooked => BookedFeatures?.Count ?? 0;
}
