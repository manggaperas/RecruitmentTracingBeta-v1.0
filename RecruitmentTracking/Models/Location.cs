using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentTracking.Models;

public class Location
{
	[Key]
	public int LocationId { get; set; }
	[Required]
	public string LocationName { get; set; }
}
