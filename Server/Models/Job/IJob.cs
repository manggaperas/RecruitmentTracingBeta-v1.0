namespace Server.Models;

public interface IJob
{
    int JobId { get; set; }
    string? JobTitle { get; set; }
    string? JobDescription { get; set; }
    string? JobRequirement { get; set; }
    bool IsJobAvailable { get; set; }
    DateTime? JobExpiredDate { get; set; }
}
