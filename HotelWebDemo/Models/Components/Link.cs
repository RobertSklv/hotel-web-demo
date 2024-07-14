namespace HotelWebDemo.Models.Components;

public class Link : Element, IDisableableElement
{
    public string Href { get; set; }

    public bool Disabled { get; set; }
}
