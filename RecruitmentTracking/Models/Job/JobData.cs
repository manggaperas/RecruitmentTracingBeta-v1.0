namespace RecruitmentTracking.Models;

public class JobData
{
    public int JobId { get; set; }
    public string? JobTitle { get; set; }
    public string? JobDescription { get; set; }
    public string? JobRequirement { get; set; }
    public string? Location { get; set; }
    public DateTime? JobPostedDate { get; set; }
    public DateTime? JobExpiredDate { get; set; }
}
