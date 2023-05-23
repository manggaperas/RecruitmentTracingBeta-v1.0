using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Please fill in the required information.")]
    [EmailAddress(ErrorMessage = "Invalid email address. Please enter a valid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please fill in the required information.")]
    public string? Password { get; set; }
}
