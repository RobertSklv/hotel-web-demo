using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("Countries")]
[SelectOption]
[JsonObject]
public class Country : BaseEntity
{
    [StringLength(64, MinimumLength = 2)]
    public string Name { get; set; }

    [MaxLength(3)]
    [Column(TypeName = "varchar")]
    public string Code { get; set; }
}
