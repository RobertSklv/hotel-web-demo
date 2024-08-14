using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingTotalsDiscounts")]
public class BookingTotalsDiscount : BaseEntity
{
    public BookingTotals? Totals { get; set; }

    public int? TotalsId { get; set; }

    [StringLength(64)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }
}
