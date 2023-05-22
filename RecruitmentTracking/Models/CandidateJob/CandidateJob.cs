using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentTracking.Models;

public class CandidateJob
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Job")]
    public int JobId { get; set; }
    
    [Required]
    [ForeignKey("Candidate")]
    public int CandidateId { get; set; }
    public string? CV { get; set; }

    public Job? Job { get; set; }
    public Candidate? Candidate { get; set; }
}
