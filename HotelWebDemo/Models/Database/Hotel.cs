using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;

namespace HotelWebDemo.Models.Database;

[Table("Hotels")]
[SelectOption]
public class Hotel : BaseEntity
{
    [StringLength(32, MinimumLength = 1)]
    [TableColumn]
    public string Name { get; set; }

    [MaxLength(128)]
    [TableColumn]
    [Display(Name = "Short description")]
    public string? ShortDescription { get; set; }

    [MaxLength(1024)]
    [TableColumn]
    [Display(Name = "Long description")]
    public string? LongDescription { get; set; }

    [Range(1, 5)]
    [TableColumn]
    public int Stars { get; set; }

    public List<Room> Rooms { get; set; }

    public List<AdminUser> AdminUsers { get; set; }

    public List<RoomCategory> Categories { get; set; }

    [TableColumn(Name = "Room count")]
    public int RoomCount => Rooms?.Count ?? 0;

    [TableColumn(Name = "Total capacity")]
    public int TotalCapacity => Rooms?.Sum(r => r.Capacity) ?? 0;

    [TableColumn(Name = "Administrators")]
    public int AdminUsersCount => AdminUsers?.Count ?? 0;
}
