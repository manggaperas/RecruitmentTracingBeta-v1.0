using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentTracking.Models;

public class UserEditProfile : IdentityUser
{
    [Required]
    public string? Name { get; set; }

    public int CandidateId { get; set; }
    public string? Phone { get; set; }
    public double GPA { get; set; }
    public string? LastEducation { get; set; }

    public string? Schedule { get; set; }
    public string? Salary { get; set; }

    public string? StatusInJob { get; set; }

    public IFormFile? FileCV { get; set; }
}
