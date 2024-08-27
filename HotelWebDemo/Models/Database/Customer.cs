using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("Customers")]
[SelectOption(LabelProperty = nameof(FullName))]
[JsonObject]
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

    [StringLength(16)]
    [Display(Name = "National ID")]
    [JsonIgnore]
    public string? NationalId { get; set; }

    [StringLength(16)]
    [Display(Name = "Passport ID")]
    [JsonIgnore]
    public string? PassportId { get; set; }

    [TableColumn]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Country? Citizenship { get; set; }

    public int CitizenshipId { get; set; }

    [Display(Name = "Date of birth")]
    public DateTime DateOfBirth { get; set; }

    [TableColumn(SpecialFormat = TableColumnSpecialFormat.MaleFemale)]
    public bool Gender { get; set; }

    [ForeignKey(nameof(AddressId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Address? Address { get; set; }

    public int AddressId { get; set; }

    [ForeignKey(nameof(CustomerAccountId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    [JsonIgnore]
    public CustomerAccount? CustomerAccount { get; set; }

    [JsonIgnore]
    public int? CustomerAccountId { get; set; }

    [JsonIgnore]
    public List<CustomerCheckinInfo>? CustomerCheckinInfos { get; set; }

    [JsonIgnore]
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

    [JsonIgnore]
    [TableColumn(Name = "E-mail", DefaultValue = "Not specified.", SortOrder = -1)]
    public string? CustomerAccount_Email => CustomerAccount?.Email;

    [JsonIgnore]
    [TableColumn(Name = "Address", DefaultValue = "Not specified.")]
    public string? Address_StreetLine1
    {
        get
        {
            StringBuilder sb = new();
            sb.AppendLine(Address?.StreetLine1);

            if (Address?.StreetLine2 != null) sb.AppendLine(Address?.StreetLine2);
            if (Address?.StreetLine3 != null) sb.AppendLine(Address?.StreetLine3);

            sb.Append(Address?.City);
            sb.Append(", ");
            sb.AppendLine(Address?.PostalCode);

            sb.AppendLine(Address?.Country?.Name);

            return sb.ToString();
        }
    }
}
