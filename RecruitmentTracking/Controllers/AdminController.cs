using Microsoft.AspNetCore.Mvc;
using log4net;

using RecruitmentTracking.Models;
using IndexDb;
using System.Data.Entity;

namespace RecruitmentTracking.Controllers;

public class AdminController : Controller
{
    private readonly DataContex _db = new();
    private readonly ILog _log;

    public AdminController()
    {
        _log = LogManager.GetLogger(typeof(AdminController));
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // [HttpGet("/NotAvailableJobs")]
    // public async Task<IEnumerable<Job>> NotAvailableJob()
    // {
    //     return await _db.Jobs!.Where(Job => !Job.IsJobAvailable).ToListAsync();
    // }

    [HttpPost("/CreateJob")]
    public async Task<IActionResult> CreateJob(JobCreate objJob)
    {
        // Admin admin = new()
        // {
        //     AdminId = objJob.AdminId,
        // };

        Job newJob = new()
        {
            JobTitle = objJob.JobTitle,
            JobDescription = objJob.JobDescription,
            JobExpiredDate = objJob.JobExpiredDate,
            JobRequirement = objJob.JobRequirement,
            JobPostedDate = DateTime.Now,
            IsJobAvailable = true,
            //Admin = admin,
        };
        _db.Jobs!.Add(newJob);
        await _db.SaveChangesAsync();

        _log.Info("Job Added.");

        return Ok(newJob);
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
