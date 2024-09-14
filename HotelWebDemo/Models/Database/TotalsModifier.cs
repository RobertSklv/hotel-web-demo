using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("TotalsModifiers")]
public class TotalsModifier : BaseEntity, IChargeable
{
    public BookingTotals? Totals { get; set; }

    public int? TotalsId { get; set; }

    [StringLength(64)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Price per night")]
    public bool IsPricePerNight { get; set; }

    public string GetLabel(int nights)
    {
        if (!IsPricePerNight)
        {
            return Name;
        }

        return $"{nights}x {Name}";
    }

    public decimal GetPrice(int nights)
    {
        if (!IsPricePerNight)
        {
            return Price;
        }

        return Price * nights;
    }
}
