using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("BookingItemRoomFeatures")]
public class BookingItemRoomFeature : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingItem BookingItem { get; set; }

    public int BookingItemId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public RoomFeature RoomFeature { get; set; }

    public int RoomFeatureId { get; set; }
}
