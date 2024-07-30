using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("Customers")]
[SelectOption(LabelProperty = nameof(FullName))]
public class Customer : BaseEntity
{
    [TableColumn]
    [Display(Name = "First name")]
    [StringLength(64, MinimumLength = 1)]
    public string FirstName { get; set; }

    [TableColumn]
    [Display(Name = "Middle name")]
    [StringLength(64, MinimumLength = 1)]
    public string? MiddleName { get; set; }

    [TableColumn]
    [Display(Name = "Last name")]
    [StringLength(64, MinimumLength = 1)]
    public string LastName { get; set; }

    [ForeignKey(nameof(CustomerIdentityId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public CustomerIdentity? CustomerIdentity { get; set; }

    public int? CustomerIdentityId { get; set; }

    [ForeignKey(nameof(CustomerAccountId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public CustomerAccount? CustomerAccount { get; set; }

    public int? CustomerAccountId { get; set; }

    public List<BookingCustomer>? BookingCustomers { get; set; }

    public List<Review>? Reviews { get; set; }

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

    [TableColumn(Name = "E-mail", DefaultValue = "Not specified.", SortOrder = -1)]
    public string? CustomerAccount_Email => CustomerAccount?.Email;

    [TableColumn(Name = "Citizenship", DefaultValue = "Not specified.")]
    public Country? CustomerIdentity_Citizenship => CustomerIdentity?.Citizenship;

    [TableColumn(Name = "Address", DefaultValue = "Not specified.")]
    public string? CustomerAccount_Address_StreetLine1
    {
        get
        {
            if (CustomerAccount == null || CustomerAccount.Address == null)
            {
                return null;
            }

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
