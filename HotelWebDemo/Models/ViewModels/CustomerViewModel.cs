using HotelWebDemo.Models.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.ViewModels;

public class CustomerViewModel : IModel
{
    public int Id { get; set; }

    [Display(Name = "First name")]
    [StringLength(64, MinimumLength = 1)]
    public string FirstName { get; set; }

    [Display(Name = "Middle name")]
    [StringLength(64, MinimumLength = 1)]
    public string? MiddleName { get; set; }

    [Display(Name = "Last name")]
    [StringLength(64, MinimumLength = 1)]
    public string LastName { get; set; }

    [StringLength(16)]
    public string? NationalId { get; set; }

    [StringLength(16)]
    public string? PassportId { get; set; }

    public Country? Citizenship { get; set; }

    public int CitizenshipId { get; set; }

    [Required]
    [StringLength(64)]
    [EmailAddress]
    [Column(TypeName = "varchar")]
    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(64)]
    [Display(Name = "Street line 1")]
    public string StreetLine1 { get; set; }

    [StringLength(64)]
    [Display(Name = "Street line 2")]
    public string? StreetLine2 { get; set; }

    [StringLength(64)]
    [Display(Name = "Street line 3")]
    public string? StreetLine3 { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Country? Country { get; set; }

    [Display(Name = "Country")]
    public int CountryId { get; set; }

    [StringLength(32)]
    public string City { get; set; }

    [StringLength(16)]
    [Display(Name = "Postal code")]
    public string PostalCode { get; set; }

    [StringLength(16)]
    public string Phone { get; set; }
}
