using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentTracking.Models;

public class User : IdentityUser
{
	public string? Name { get; set; }

}
