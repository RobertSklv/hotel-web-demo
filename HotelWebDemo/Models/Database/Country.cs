using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("Countries")]
public class Country : BaseEntity
{
    [StringLength(64, MinimumLength = 2)]
    public string Name { get; set; }

    [MaxLength(3)]
    [Column(TypeName = "varchar")]
    public string Code { get; set; }
}
