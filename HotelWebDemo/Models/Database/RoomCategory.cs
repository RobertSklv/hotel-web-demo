using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("RoomCategories")]
public class RoomCategory : BaseEntity
{
    [StringLength(32, MinimumLength = 1)]
    public string Name { get; set; }

    [MaxLength(128)]
    public string? ShortDescription { get; set; }

    [MaxLength(1024)]
    public string? LongDescription { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Price { get; set; }

    public List<Room> Rooms { get; set; }
}
