using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTracking.Data;
using RecruitmentTracking.Models;

namespace RecruitmentTracking.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<JobViewModel> listJob = new();
        foreach (Job job in _context.Jobs!.Where(j => j.IsJobAvailable).ToList())
        {
            JobViewModel data = new()
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                JobDescription = job.JobDescription,
                JobRequirement = job.JobRequirement,
                Location = job.Location,
                JobPostedDate = job.JobPostedDate,
                JobExpiredDate = job.JobExpiredDate,
            };

            listJob.Add(data);
        }
        return View(listJob);
    }

    [HttpGet("/DetailJob/{id}")]
    public IActionResult DetailJob(int id)
    {
        Job objJob = _context.Jobs!.Find(id)!;

        JobViewModel data = new()
        {
            JobId = objJob.JobId,
            JobTitle = objJob.JobTitle,
            JobDescription = objJob.JobDescription,
            JobRequirement = objJob.JobRequirement,
            Location = objJob.Location,
            JobPostedDate = objJob.JobPostedDate,
            JobExpiredDate = objJob.JobExpiredDate,
        };

        return View(data);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
