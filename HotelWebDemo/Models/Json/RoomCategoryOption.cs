using Newtonsoft.Json;

namespace HotelWebDemo.Models.Json;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class RoomCategoryOption
{
    [JsonProperty]
    public int Id { get; set; }

    [JsonProperty]
    public string Name { get; set; }

    [JsonProperty]
    public int HotelId { get; set; }
}
