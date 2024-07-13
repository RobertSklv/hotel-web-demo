using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.Database;

[Table("CustomerAccounts")]
public class CustomerAccount : BaseEntity
{
    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    [Required]
    [StringLength(64)]
    [EmailAddress]
    [Column(TypeName = "varchar")]
    public string Email { get; set; }

    [StringLength(60)]
    [Column(TypeName = "varchar")]
    public string PasswordHash { get; set; }

    [MinLength(16)]
    [MaxLength(16)]
    public byte[] PasswordHashSalt { get; set; }

    public DateTime DateOfBirth { get; set; }

    [ForeignKey(nameof(AddressId))]
    public Address Address { get; set; }

    public int AddressId { get; set; }
}
