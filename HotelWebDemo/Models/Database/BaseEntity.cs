using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelWebDemo.Models.Attributes;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[JsonObject(MemberSerialization = MemberSerialization.OptOut)]
public abstract class BaseEntity : IBaseEntity
{
    [Column(Order = 0)]
    [TableColumn(Name = "#", SortOrder = -10, Searchable = false)]
    public int Id { get; set; }

    [JsonIgnore]
    [TableColumn(SortOrder = 998, Format = "dd MMM yyyy hh:mm:ss")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    [TableColumn(SortOrder = 999, Format = "dd MMM yyyy hh:mm:ss")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Updated at")]
    public DateTime UpdatedAt { get; set; }
}
