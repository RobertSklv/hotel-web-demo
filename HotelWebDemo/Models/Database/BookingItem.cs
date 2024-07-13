using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingItems")]
public class BookingItem : BaseEntity
{
    public Booking Booking { get; set; }

    public int BookingId { get; set; }

    public RoomCategory RoomCategory { get; set; }

    public int RoomCategoryId { get; set; }

    [Range(1, 999)]
    public int Quantity { get; set; }

    public List<BookingItemRoomFeature> DesiredFeatures { get; set; }
}
