namespace RecruitmentTracking.Models;

public class DataCandidateJobs
{
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? LastEducation { get; set; }
    public double GPA { get; set; }
    public string? CV { get; set; }
    public string? Salary { get; set; }

    public int CandidateJobId { get; set; }
}
