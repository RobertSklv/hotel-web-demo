namespace HotelWebDemo.Models.Components;

public class Element
{
    public string Id { get; set; }

    public List<string> ClassList { get; set; } = new();

    public string? Class => ClassList.Any() ? string.Join(' ', ClassList) : null;
}
