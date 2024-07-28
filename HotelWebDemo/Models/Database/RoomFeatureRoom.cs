using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("RoomFeatureRooms")]
public class RoomFeatureRoom : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Room Room { get; set; }

    public int RoomId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public RoomFeature RoomFeature { get; set; }

    public int RoomFeatureId { get; set; }
}
