using HotelWebDemo.Models.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.Database.Indexing;

public class BaseIndexEntity : IBaseEntity
{
    [Column(Order = 0)]
    [TableColumn(Name = "#", SortOrder = -10)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
