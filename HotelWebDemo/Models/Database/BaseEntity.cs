using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.Database;

public abstract class BaseEntity
{
    [Column(Order = 0)]
    public int Id { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Created at")]
    public DateTime CreatedAt { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Updated at")]
    public DateTime UpdatedAt { get; set; }
}
