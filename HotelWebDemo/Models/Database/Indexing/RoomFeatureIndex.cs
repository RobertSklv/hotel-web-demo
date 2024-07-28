using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.Database.Indexing;

[Table("Indexed_RoomFeatures")]
public class RoomFeatureIndex : BaseIndexEntity
{
    [StringLength(32, MinimumLength = 1)]
    [Column(TypeName = "varchar")]
    [RegularExpression("^(?:[A-Za-z][A-Za-z0-9]*(?:_[A-Za-z][A-Za-z0-9]*)*)$", ErrorMessage = "Invalid code.")]
    [TableColumn]
    public string Code { get; set; }

    [StringLength(64, MinimumLength = 1)]
    [TableColumn]
    public string Name { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    [TableColumn]
    public decimal Price { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [TableColumn]
    public Hotel Hotel { get; set; }

    public int HotelId { get; set; }

    [TableColumn(Name = "Times used")]
    public int TimesUsed { get; set; }

    [TableColumn(Name = "Times booked")]
    public int TimesBooked { get; set; }
}
