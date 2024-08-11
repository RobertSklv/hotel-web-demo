using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingContacts")]
public class BookingContact : BaseEntity
{
    [Display(Name = "Full name")]
    [StringLength(64)]
    public string FullName { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Column(TypeName = "VARCHAR")]
    [StringLength(32)]
    public string? Phone { get; set; }

    [EmailAddress]
    [Display(Name = "E-mail")]
    [Column(TypeName = "VARCHAR")]
    [StringLength(64)]
    public string? Email { get; set; }

    [StringLength(1024)]
    public string? Note { get; set; }
}
