using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;

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

    public Job? Job { get; set; }
    public Candidate? Candidate { get; set; }
}