using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

public class BookingEventLog : BaseEntity
{
    public Booking? Booking { get; set; }

    public int BookingId { get; set; }

    [StringLength(64)]
    [Column(TypeName = "VARCHAR")]
    public string Message { get; set; }
}
