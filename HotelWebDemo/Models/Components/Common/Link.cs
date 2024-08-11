namespace HotelWebDemo.Models.Components.Common;

public class Link : Element, IDisableableElement
{
    public string? Href { get; set; }

    public string? Area { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public Dictionary<string, string?>? QueryParameters { get; set; }

    public bool Disabled { get; set; }
}
