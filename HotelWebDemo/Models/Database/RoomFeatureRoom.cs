using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("RoomFeatureRooms")]
public class RoomFeatureRoom : BaseEntity
{
    public Room Room { get; set; }

    public int RoomId { get; set; }

    public RoomFeature RoomFeature { get; set; }

    public int RoomFeatureId { get; set; }
}
