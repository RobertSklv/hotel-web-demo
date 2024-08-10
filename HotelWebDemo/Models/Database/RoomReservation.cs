using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingRooms")]
public class RoomReservation : BaseEntity
{
    public Booking? Booking { get; set; }

    public int BookingId { get; set; }

    public Room? Room { get; set; }

    public int RoomId { get; set; }
}
