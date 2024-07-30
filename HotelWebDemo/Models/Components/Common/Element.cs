namespace HotelWebDemo.Models.Components.Common;

public class Element
{
    public string Id { get; set; }

    public List<string> ClassList { get; set; } = new();

    public string? Content { get; set; }

    public ColorClass? Color { get; set; }

    public string ColorClass => Color?.ToString()?.ToLower() ?? string.Empty;

    public string? Class
    {
        get
        {
            List<string> classList = new(ClassList)
            {
                ColorClass
            };
            return ClassList.Any() ? string.Join(' ', classList) : null;
        }
    }
}
