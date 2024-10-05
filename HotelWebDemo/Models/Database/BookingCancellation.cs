using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingCancellations")]
public class BookingCancellation : BaseEntity
{
    public Booking? Booking { get; set; }

    [StringLength(2048, MinimumLength = 10)]
    public string Reason { get; set; }
}
