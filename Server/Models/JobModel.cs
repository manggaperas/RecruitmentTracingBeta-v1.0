namespace Server.Models;

public class JobModel
{
    [Key]
    public int JobId { get; set; }

    [Required]
    public string? JobTitle { get; set; }

    [Required]
    public string? JobDescription { get; set; }

    [Required]
    public string? JobRequirement { get; set; }

    [Required]
    public bool IsJobAvailable { get; set; }

    [ForeignKey("Admin")]
    public int AdminId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? JobPostedDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? JobExpiredDate { get; set; }
}
