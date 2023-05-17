using Microsoft.AspNetCore.Mvc;
using log4net;

using RecruitmentTracking.Models;
using IndexDb;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentTracking.Controllers;

public class CandidateController : Controller
{
    private readonly DataContex _db = new();
    private readonly ILog _log;

    public CandidateController()
    {
        _log = LogManager.GetLogger(typeof(CandidateController));
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/Job")]
    public IActionResult DetailJob()
    {
        return View();
    }

    [HttpGet("/Profile")]
    public IActionResult EditProfile()
    {
        return View();
    }

    [HttpGet("/Jobs/{id}")]
    public IActionResult Jobs()
    {
        return View();
    }

    // [HttpGet("/Jobs")]
    // public async Task<IEnumerable<Job>> Job()
    // {
    //     return await _db.Jobs!.Where(Job => Job.IsJobAvailable).ToListAsync();
    // }

    // [HttpPatch("/EditProfile")]
    // public async Task<IActionResult> EditProfile(Candidate objCandidate)
    // {
    //     _db.Entry(objCandidate).State = EntityState.Modified;
    //     await _db.SaveChangesAsync();

    //     _log.Info("Job Updated.");

    //     return Ok(objCandidate);
    // }
}
