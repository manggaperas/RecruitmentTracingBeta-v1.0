using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using log4net;

using RecruitmentTracking.Models;
using IndexDb;
using System.Data.Entity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RecruitmentTracking.Controllers;

public class AdminController : Controller
{
    private readonly DataContex _db = new();
    private readonly ILog _log;
    private readonly IConfiguration _configuration;

    public AdminController(IConfiguration configuration)
    {
        _log = LogManager.GetLogger(typeof(AdminController));
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

        List<JobData> listJob = new();
        foreach (Job job in _db.Jobs!.ToList())
        {
            JobData data = new()
            {
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

    [HttpGet("/JobClosed")]
    public IActionResult JobClosed()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

        return View();
    }

    [HttpGet("/CreateJob")]
    public IActionResult CreateJob()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateJobs(JobCreate objJob)
    {
        string token = Request.Cookies["ActionLogin"]!;
        string email = GetEmail(token);

        Admin admin = _db.Admins!.FirstOrDefault(a => a.Email == email)!;
        Job newJob = new()
        {
            JobTitle = objJob.JobTitle,
            JobDescription = objJob.JobDescription,
            JobExpiredDate = objJob.JobExpiredDate,
            JobRequirement = objJob.JobRequirement,
            JobPostedDate = DateTime.Now,
            Location = objJob.Location,
            IsJobAvailable = true,
            Admin = admin,
        };
        _db.Jobs!.Add(newJob);
        await _db.SaveChangesAsync();

        _log.Info("Job Added.");

        return Redirect("/Admin");
    }

    private string GetEmail(string token)
    {
        ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler()
            .ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                    _configuration.GetSection("AppSettings:TokenAdmin").Value!
                                    )),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out _);

        return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value!;
    }

    // [HttpPatch("/Update/{id}")]
    // public async Task<IActionResult> UpdateJob(JobCreate objJob)
    // {
    //     _db.Entry(objJob).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
    //     await _db.SaveChangesAsync();

    //     _log.Info("Job Updated.");

    //     return Ok(objJob);
    // }

    // [HttpDelete("/Delete/{id}")]
    // public async Task<IActionResult> DeleteJob(int id)
    // {
    //     Job job = _db.Jobs!.Find(id)!;
    //     if (job != null)
    //     {
    //         _db.Jobs.Remove(job);
    //         await _db.SaveChangesAsync();

    //         _log.Info("Job deleted.");

    //         return Ok(true);
    //     }
    //     return BadRequest(false);
    // }
}
