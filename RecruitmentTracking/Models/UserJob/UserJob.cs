using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class UserJob
{
    [Key]
    public int Id { get; set; }

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
