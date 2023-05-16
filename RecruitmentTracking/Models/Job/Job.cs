using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Job : IJob, IEmailTemplate
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

    [Required]
    [DataType(DataType.Date)]
    public DateTime? JobPostedDate { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime? JobExpiredDate { get; set; }

    public string? EmailHR { get; set; }
    public string? EmailUser { get; set; }
    public string? EmailOffering { get; set; }


    [ForeignKey("Admin")]
    public int AdminId { get; set; }
    public Admin? Admin { get; set; }
}