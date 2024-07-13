using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("Addresses")]
public class Address : BaseEntity
{
    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    [Required]
    [StringLength(64)]
    public string StreetLine1 { get; set; }

    [StringLength(64)]
    public string StreetLine2 { get; set; }

    [StringLength(64)]
    public string StreetLine3 { get; set; }

    public Country Country { get; set; }

    public int CountryId { get; set; }

    [StringLength(32)]
    public string City { get; set; }

    [StringLength(16)]
    public string PostalCode { get; set; }

    [StringLength(16)]
    public string Phone { get; set; }
}
