using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class MassAction
{
    public string ActionId { get; set; }

    public string Controller { get; set; }

    public string Label { get; set; }

    public ColorClass Color { get; set; }

    public string ColorClass => Color.ToString().ToLower();

    public int SortOrder { get; set; }
}
