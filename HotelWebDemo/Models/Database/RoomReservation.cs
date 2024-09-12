using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("RoomReservations")]
public class RoomReservation : BaseEntity
{
    public Booking? Booking { get; set; }

    public int BookingId { get; set; }

    public Room? Room { get; set; }

    public int RoomId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingItem? BookingItem { get; set; }

    public int BookingItemId { get; set; }

    [ForeignKey(nameof(CheckinInfoId))]
    public CheckinInfo? CheckinInfo { get; set; }

    public int? CheckinInfoId { get; set; }
}
