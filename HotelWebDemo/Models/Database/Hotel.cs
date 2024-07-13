using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("Hotels")]
public class Hotel : BaseEntity
{
    [StringLength(32, MinimumLength = 1)]
    public string Name { get; set; }

    [MaxLength(128)]
    public string? ShortDescription { get; set; }

    [MaxLength(1024)]
    public string? LongDescription { get; set; }

    public List<Room> Rooms { get; set; }
}
