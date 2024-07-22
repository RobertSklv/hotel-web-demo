using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Pages;

public class PageActionButton
{
    public string? Action { get; set; }

    public string? Controller { get; set; }

    public string Area { get; set; } = "Admin";

    public ColorClass Color { get; set; }

    public bool IsLink { get; set; }

    public string ColorClass => Color.ToString().ToLower();

    public string? Content { get; set; }

    public bool AlignToLeft { get; set; }

    public int SortOrder { get; set; }

    public Dictionary<string, object>? RequestParameters { get; set; }
}
