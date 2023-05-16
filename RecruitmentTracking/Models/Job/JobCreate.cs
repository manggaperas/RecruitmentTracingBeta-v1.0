namespace RecruitmentTracking.Models;

public class JobCreate
{
    public int JobId { get; set; }
    public string? JobTitle { get; set; }
    public string? JobDescription { get; set; }
    public string? JobRequirement { get; set; }
    public bool IsJobAvailable { get; set; }
    public DateTime? JobExpiredDate { get; set; }
}
