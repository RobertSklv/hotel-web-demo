using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.Attributes;

public class SelectOptionAttribute : Attribute
{
    public string IdentityProperty { get; set; } = nameof(BaseEntity.Id);

    public string LabelProperty { get; set; } = "Name";

    public string UndefinedLabel { get; set; } = "Not specified.";
}
