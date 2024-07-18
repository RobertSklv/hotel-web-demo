using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.ViewModels;

public class ResetPasswordModel
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Token { get; set; }
}
