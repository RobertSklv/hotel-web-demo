﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.Database;

[Table("AdminUsers")]
public class AdminUser : BaseEntity
{
    [StringLength(32, MinimumLength = 2)]
    [Column(TypeName = "varchar")]
    public string UserName { get; set; }

    [StringLength(64, MinimumLength = 6)]
    [EmailAddress]
    [Column(TypeName = "varchar")]
    public string Email { get; set; }

    [StringLength(60)]
    [Column(TypeName = "varchar")]
    public string PasswordHash { get; set; }

    [MinLength(16)]
    [MaxLength(16)]
    public byte[] PasswordHashSalt { get; set; }

    public AdminUserRole Role { get; set; }

    [MaxLength(64)]
    [Column(TypeName = "varchar")]
    public string? ProfileImagePath { get; set; }

    public DateTime? DateOfBirth { get; set; } = null;
}