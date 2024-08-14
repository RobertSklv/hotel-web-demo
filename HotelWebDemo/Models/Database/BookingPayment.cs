﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("BookingPayments")]
public class BookingPayment : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Booking? Booking { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Customer? Customer { get; set; }

    public int CustomerId { get; set; }

    [StringLength(64)]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    [StringLength(64)]
    [Display(Name = "Full name")]
    public string FullName { get; set; }

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
