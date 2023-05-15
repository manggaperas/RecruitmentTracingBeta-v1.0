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
    private readonly IndexDbContex _db = new();
    private readonly ILog _log;

    public CandidateController()
    {
        _log = LogManager.GetLogger(typeof(CandidateController));
    }

    [HttpGet]
    [Route("/Jobs")]
    public async Task<IEnumerable<Job>> Job()
    {
        return await _db.Jobs!.Where(Job => Job.IsJobAvailable).ToListAsync();
    }

    [HttpPatch]
    [Route("/EditProfile")]
    public async Task<Candidate> EditProfile(Candidate objCandidate)
    {
        _db.Entry(objCandidate).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
        await _db.SaveChangesAsync();

        _log.Info("Job Updated.");

        return objCandidate;
    }
}
