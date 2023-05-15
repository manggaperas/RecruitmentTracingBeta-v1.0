using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class Admin
{
    [Key]
    public int AdminId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
