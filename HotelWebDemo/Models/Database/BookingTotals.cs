using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("BookingTotals")]
public class BookingTotals : BaseEntity
{
    [JsonIgnore]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Booking? Booking { get; set; }

    [Range(1, 999)]
    public int Nights { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    [Display(Name = "Custom grand total")]
    public decimal? CustomGrandTotal { get; set; }

    public List<TotalsModifier>? Modifiers { get; set; }

    [Display(Name = "Has custom grand total")]
    public bool HasCustomGrandTotal => CustomGrandTotal != null;
}
