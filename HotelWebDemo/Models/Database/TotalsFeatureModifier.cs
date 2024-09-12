using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("TotalsFeatureModifiers")]
public class TotalsFeatureModifier : TotalsModifier
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public RoomFeature? Feature { get; set; }

    public int? FeatureId { get; set; }
}
