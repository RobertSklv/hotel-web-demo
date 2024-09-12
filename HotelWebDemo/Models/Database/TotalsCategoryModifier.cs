using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("TotalsCategoryModifiers")]
public class TotalsCategoryModifier : TotalsModifier
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public RoomCategory? Category { get; set; }

    public int? CategoryId { get; set; }
}
