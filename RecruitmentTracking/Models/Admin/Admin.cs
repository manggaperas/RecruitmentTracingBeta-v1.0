using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class Admin
{
    [Key]
    public int AdminId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
