using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentTracking.Models;

public class Candidate
{
    [Key]
    public int CandidateId { get; set; }
    public string? Phone { get; set; }
    public double GPA { get; set; }
    public string? LastEducation { get; set; }

    public string? Schedule { get; set; }
    public string? Salary { get; set; }

    public string? StatusInJob { get; set; }

    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User? User { get; set; }
}
