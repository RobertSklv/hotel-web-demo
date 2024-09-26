using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("RoomReservations")]
public class RoomReservation : BaseEntity
{
    [TableColumn]
    public Booking? Booking { get; set; }

    public int BookingId { get; set; }

    [TableColumn]
    public Room? Room { get; set; }

    public int RoomId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingItem? BookingItem { get; set; }

    public int BookingItemId { get; set; }

    [ForeignKey(nameof(CheckinInfoId))]
    public CheckinInfo? CheckinInfo { get; set; }

    public int? CheckinInfoId { get; set; }

    public string RoomCheckinTitle
    {
        get
        {
            if (Room == null) throw new Exception($"The room is not loaded.");
            if (Room.Category == null) throw new Exception($"The category is not loaded.");

            string adultsLabel = Room.Capacity > 1 ? "adults" : "adult";

            return $"{Room.Category.Name}, {Room.Capacity} {adultsLabel}, room No. {Room.Number}";
        }
    }
}
