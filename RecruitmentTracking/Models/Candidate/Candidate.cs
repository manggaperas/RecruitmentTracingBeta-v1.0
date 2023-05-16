using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class Candidate
{
    [Key]
    public int CandidateId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    public string? Phone { get; set; }
    public string? LastEducation { get; set; }
}
