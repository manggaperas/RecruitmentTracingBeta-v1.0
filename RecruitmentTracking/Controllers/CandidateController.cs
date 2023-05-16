using Microsoft.AspNetCore.Mvc;
using log4net;

using Server.Models;
using IndexDb;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidateController : ControllerBase
{
    private readonly DataContex _db = new();
    private readonly ILog _log;

    public CandidateController()
    {
        _log = LogManager.GetLogger(typeof(CandidateController));
    }

    [HttpGet("/Jobs")]
    public async Task<IEnumerable<Job>> Job()
    {
        return await _db.Jobs!.Where(Job => Job.IsJobAvailable).ToListAsync();
    }

    [HttpPatch("/EditProfile")]
    public async Task<IActionResult> EditProfile(Candidate objCandidate)
    {
        _db.Entry(objCandidate).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        _log.Info("Job Updated.");

        return Ok(objCandidate);
    }
}
