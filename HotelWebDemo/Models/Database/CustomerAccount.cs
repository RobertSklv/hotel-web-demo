﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("CustomerAccounts")]
public class CustomerAccount : BaseEntity
{
    public Customer? Customer { get; set; }

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

    [MinLength(12)]
    [MaxLength(12)]
    public byte[]? PasswordResetToken { get; set; }

    public DateTime? PasswordResetStart { get; set; }

    public bool EmailVerified { get; set; }

    public List<Review>? Reviews { get; set; }
}
