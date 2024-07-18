using System.ComponentModel.DataAnnotations;

namespace HotelWebDemo.Models.ViewModels;

public class LoginCredentials
{
    [Required]
    [Display(Name = "Username or e-mail")]
    public string UserNameOrEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
