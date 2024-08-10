namespace HotelWebDemo.Models.Components.Common;

public class BootstrapIcon : Element
{
    public BootstrapIconType Type { get; set; }

    public string GetIconClass()
    {
        return Type switch
        {
            BootstrapIconType.None => string.Empty,
            BootstrapIconType.Pencil => "bi-pencil-fill",
            BootstrapIconType.TrashCan => "bi-trash-fill",
            _ => string.Empty
        };
    }
}
