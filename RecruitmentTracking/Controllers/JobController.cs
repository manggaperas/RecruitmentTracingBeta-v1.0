using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecruitmentTracking.Data;
using RecruitmentTracking.Models;

namespace RecruitmentTracking.Controllers;

public class JobController : Controller
{
	private readonly ApplicationDbContext _context;
	private readonly ILogger<HomeController> _logger;
	private readonly IConfiguration _configuration;
	private readonly UserManager<User> _userManager;
	
	public JobController(ILogger<HomeController> logger, IConfiguration configuration, UserManager<User> userManager, ApplicationDbContext context)
	{
		_logger = logger;
		_configuration = configuration;
		_userManager = userManager;
		_context = context;
	}
	
	[HttpGet]
	public IActionResult Index(string? searchstring)
	{
		var jobs = from j in _context.Jobs select j;
		
		if(!String.IsNullOrEmpty(searchstring))
		{
			jobs = jobs.Where(j => j.JobTitle.Contains(searchstring));
		}
		
		return View();
	}
}
