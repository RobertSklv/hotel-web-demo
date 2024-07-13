using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("CustomerIdentities")]
public class CustomerIdentity : BaseEntity
{
    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    [StringLength(16)]
    public string? NationalId { get; set; }

    [StringLength(16)]
    public string? PassportId { get; set; }

    public Country Citizenship { get; set; }
}
