using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelWebDemo.Models.Attributes;

namespace HotelWebDemo.Models.Database;

public abstract class BaseEntity : IBaseEntity
{
    [Column(Order = 0)]
    [TableColumn(Name = "#", SortOrder = -10, Searchable = false)]
    public int Id { get; set; }

    [TableColumn(SortOrder = 998)]
    [DataType(DataType.DateTime)]
    [Display(Name = "Created at")]
    public DateTime CreatedAt { get; set; }

    [TableColumn(SortOrder = 999)]
    [DataType(DataType.DateTime)]
    [Display(Name = "Updated at")]
    public DateTime UpdatedAt { get; set; }
}
