using Microsoft.AspNetCore.Mvc;
using log4net;

using Server.Models;
using IndexDb;
using System.Data.Entity;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IndexDbContex _db = new();
    private readonly ILog _log;

    public AdminController()
    {
        _log = LogManager.GetLogger(typeof(AdminController));
    }

    [HttpGet]
    [Route("/AvailableJobs")]
    public async Task<IEnumerable<Job>> AvailbaleJob()
    {
        return await _db.Jobs!.Where(Job => Job.IsJobAvailable).ToListAsync();
    }

    [HttpGet]
    [Route("/NotAvailableJobs")]
    public async Task<IEnumerable<Job>> NotAvailableJob()
    {
        return await _db.Jobs!.Where(Job => !Job.IsJobAvailable).ToListAsync();
    }

    [HttpPost]
    [Route("/CreateJob")]
    public async Task<Job> CreateJob(Job objJob)
    {
        Admin admin = new()
        {
            AdminId = objJob.AdminId,
        };

        Job newJob = new()
        {
            JobTitle = objJob.JobTitle,
            JobDescription = objJob.JobDescription,
            JobExpiredDate = objJob.JobExpiredDate,
            JobRequirement = objJob.JobRequirement,
            JobPostedDate = DateTime.Now,
            IsJobAvailable = true,
            Admin = admin,
        };
        _db.Jobs!.Add(newJob);
        await _db.SaveChangesAsync();

        _log.Info("Job Added.");

        return newJob;
    }

    [HttpPatch]
    [Route("/Update/{id}")]
    public async Task<Job> UpdateJob(Job objJob)
    {
        _db.Entry(objJob).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
        await _db.SaveChangesAsync();

        _log.Info("Job Updated.");

        return objJob;
    }

    [HttpDelete]
    [Route("/Delete/{id}")]
    public async Task<bool> DeleteJob(int id)
    {
        Job job = _db.Jobs!.Find(id)!;
        if (job != null)
        {
            _db.Jobs.Remove(job);
            await _db.SaveChangesAsync();

            _log.Info("Job deleted.");

            return true;
        }
        return false;
    }
}
