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

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal RoomsPrice { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal RoomFeaturesPrice { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    [Display(Name = "Grand total")]
    public decimal? CustomGrandTotal { get; set; }

    public List<BookingTotalsDiscount>? Discounts { get; set; }

    [Display(Name = "Has custom grand total")]
    public bool HasCustomGrandTotal => CustomGrandTotal != null;

    public decimal GetGrandTotal()
    {
        if (Discounts == null)
        {
            throw new Exception("Discounts must be loaded in order to calculate the grand total.");
        }

        decimal total = RoomsPrice + RoomFeaturesPrice + Tax;

        foreach (BookingTotalsDiscount d in Discounts)
        {
            total -= d.Amount;
        }

        return total;
    }
}
