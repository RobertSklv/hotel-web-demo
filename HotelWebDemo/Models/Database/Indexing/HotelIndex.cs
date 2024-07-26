using HotelWebDemo.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database.Indexing;

[Table("Indexed_Hotels")]
public class HotelIndex : BaseIndexEntity
{
    [StringLength(32, MinimumLength = 1)]
    [TableColumn]
    public string Name { get; set; }

    [MaxLength(128)]
    [TableColumn(Name = "Short description")]
    public string? ShortDescription { get; set; }

    [MaxLength(1024)]
    [TableColumn(Name = "Long description")]
    public string? LongDescription { get; set; }

    [Range(1, 5)]
    [TableColumn]
    public int Stars { get; set; }

    [TableColumn(Name = "Room count")]
    public int RoomCount { get; set; }

    [TableColumn(Name = "Total capacity")]
    public int TotalCapacity { get; set; }

    [TableColumn(Name = "Administrators")]
    public int AdminUsersCount { get; set; }
}
