using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("Customers")]
public class Customer : BaseEntity
{
    [StringLength(64, MinimumLength = 1)]
    public string FirstName { get; set; }

    [StringLength(64, MinimumLength = 1)]
    public string? MiddleName { get; set; }

    [StringLength(64, MinimumLength = 1)]
    public string LastName { get; set; }

    [ForeignKey(nameof(CustomerIdentityId))]
    public CustomerIdentity? CustomerIdentity { get; set; }

    public int? CustomerIdentityId { get; set; }

    [ForeignKey(nameof(CustomerAccountId))]
    public CustomerAccount? CustomerAccount { get; set; }

    public int? CustomerAccountId { get; set; }

    public List<BookingCustomer> BookingCustomers { get; set; }

    public List<Review> Reviews { get; set; }

    public string FullName
    {
        get
        {
            string fullName = FirstName;
            
            if (MiddleName != null)
            {
                fullName += " " + MiddleName;
            }

            fullName += " " + LastName;

            return fullName;
        }
    }
}
