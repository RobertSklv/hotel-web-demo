using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingItemRoomFeatures")]
public class BookingItemRoomFeature : BaseEntity
{
    public BookingItem BookingItem { get; set; }

    public int BookingItemId { get; set; }

    public RoomFeature RoomFeature { get; set; }

    public int RoomFeatureid { get; set; }
}
