using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class CandidateEditProfile
{
    [Required(ErrorMessage = "Please fill in the required information.")]
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? LastEducation { get; set; }
    public double GPA { get; set; }
    public IFormFile? FileCV { get; set; }
}
