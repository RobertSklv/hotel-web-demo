namespace HotelWebDemo.Models.Database;

public class AdminRole : BaseEntity
{
    public string Code { get; set; }

    public string DisplayedName { get; set; }

    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }
}
