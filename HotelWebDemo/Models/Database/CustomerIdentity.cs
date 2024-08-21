using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("CustomerIdentities")]
public class CustomerIdentity : BaseEntity
{
    public Customer? Customer { get; set; }

    public int CustomerId { get; set; }

    [StringLength(16)]
    public string? NationalId { get; set; }

    [StringLength(16)]
    public string? PassportId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Country? Citizenship { get; set; }

    public int CitizenshipId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool Gender { get; set; }
}
