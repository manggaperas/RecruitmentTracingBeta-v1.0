using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class Candidate
{
    [Key]
    public int CandidateId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    public string? LastEducation { get; set; }
}
