using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("Rooms")]
public class Room : BaseEntity
{
    public Hotel Hotel { get; set; }

    public int HotelId { get; set; }

    public RoomCategory Category { get; set; }

    public int CategoryId { get; set; }

    public int Floor { get; set; }

    public int Number { get; set; }

    public float Area { get; set; }

    [Range(0, 10)]
    public int Capacity { get; set; }

    public List<RoomFeatureRoom> RoomFeatureRooms { get; set; }
}
