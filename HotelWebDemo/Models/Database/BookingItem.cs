using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("BookingItems")]
public class BookingItem : BaseEntity
{
    public Booking Booking { get; set; }

    public int BookingId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public RoomCategory? RoomCategory { get; set; }

    public int RoomCategoryId { get; set; }

    [Range(1, 5)]
    public int TargetCapacity { get; set; }

    [Range(1, 999)]
    public int Quantity { get; set; }

    public List<BookingItemRoomFeature> DesiredFeatures { get; set; }
}
