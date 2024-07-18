using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

    public string Address
    {
        get
        {
            StringBuilder sb = new();
            sb.AppendLine(CustomerAccount?.Address?.StreetLine1);

            if (CustomerAccount?.Address?.StreetLine2 != null) sb.AppendLine(CustomerAccount?.Address?.StreetLine2);
            if (CustomerAccount?.Address?.StreetLine3 != null) sb.AppendLine(CustomerAccount?.Address?.StreetLine3);

            sb.Append(CustomerAccount?.Address?.City);
            sb.Append(", ");
            sb.AppendLine(CustomerAccount?.Address?.PostalCode);

            sb.AppendLine(CustomerAccount?.Address?.Country?.Name);

            return sb.ToString();
        }
    }
}
