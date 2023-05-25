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

    public DateTime? DateHRInterview { get; set; }

    public DateTime? TimeHRInterview { get; set; }
    public string? LocationHRInterview { get; set; }

    public DateTime? DateUserInterview { get; set; }

    public DateTime? TimeUserInterview { get; set; }
    public string? LocationUserInterview { get; set; }

    public string? EmailInterviewUser { get; set; }
    public bool SendEmailStatus { get; set; }

    public int CandidateJobId { get; set; }
}
