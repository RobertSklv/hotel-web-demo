using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingCancellations")]
public class BookingCancellation : BaseEntity
{
    public BookingPayment BookingPayment { get; set; }

    public int BookingPaymentId { get; set; }

    [StringLength(2048, MinimumLength = 10)]
    public string Reason { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal Fee { get; set; }
}
