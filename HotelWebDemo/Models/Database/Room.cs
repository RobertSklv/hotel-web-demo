using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("Rooms")]
public class Room : BaseEntity
{
    [TableColumn]
    public bool Enabled { get; set; } = true;

    [TableColumn]
    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }

    [TableColumn]
    public RoomCategory? Category { get; set; }

    public int CategoryId { get; set; }

    [TableColumn]
    public int Number { get; set; }

    [TableColumn]
    public int Floor { get; set; }

    [TableColumn(SpecialFormat = TableColumnSpecialFormat.MeterSquared)]
    public float Area { get; set; }

    [Range(0, 10)]
    [TableColumn]
    public int Capacity { get; set; }

    [JsonIgnore]
    public List<RoomFeatureRoom>? RoomFeatureRooms { get; set; }

    public List<RoomFeature>? Features { get; set; }

    [JsonIgnore]
    public List<RoomReservation>? Reservations { get; set; }

    [NotMapped]
    public List<int> SelectedFeatureIds { get; set; } = new();

    [TableColumn(Name = "Premium features", Orderable = false, Filterable = false)]
    public string FeaturesEnumerated => Features != null && Features.Count > 0
        ? string.Join(", ", Features.ConvertAll(f => $"{f.Name} (${f.Price:#0.00})"))
        : "None";
}
