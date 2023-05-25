using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class UserJob
{
    [Key]
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateHRInterview { get; set; }
    [DataType(DataType.Date)]
    public DateTime? TimeHRInterview { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateUserInterview { get; set; }
    [DataType(DataType.Date)]
    public DateTime? TimeUserInterview { get; set; }

    public string? EmailInterviewUser { get; set; }
    public bool SendEmailStatus { get; set; }

    [Required]
    [ForeignKey("Job")]
    public int JobId { get; set; }

    [Required]
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public string? CV { get; set; }

    public Job? Job { get; set; }
    public User? User { get; set; }

    public string? StatusInJob { get; set; }
}
