namespace HotelWebDemo.Models.Components.Common;

public class Link : RouteElement, IDisableableElement
{
    public string? Target { get; set; }

    public bool Disabled { get; set; }
}
