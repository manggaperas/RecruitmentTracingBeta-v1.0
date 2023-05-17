using Microsoft.AspNetCore.Mvc;
using log4net;

using RecruitmentTracking.Models;
using IndexDb;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace RecruitmentTracking.Controllers;

public class CandidateController : Controller
{
    private readonly DataContex _db = new();
    private readonly ILog _log;
    private readonly IConfiguration _configuration;

    public CandidateController(IConfiguration configuration)
    {
        _log = LogManager.GetLogger(typeof(CandidateController));
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

        return View();
    }

    [HttpGet("/Job")]
    public IActionResult DetailJob()
    {
        return View();
    }

    [HttpGet("/Profile")]
    public async Task<IActionResult> Profile()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
        if (!ViewBag.IsAuth) return Redirect("/Login");

        string token = Request.Cookies["ActionLogin"]!;

        string email = GetEmail(token);
        Candidate candidate = (await _db.Candidates!.FirstOrDefaultAsync(c => c.Email == email))!;
        CandidateEditProfile profile = new()
        {
            Name = candidate.Name,
            Email = candidate.Email,
            Phone = candidate.Phone,
            LastEducation = candidate.LastEducation,
            GPA = candidate.GPA,
        };

        return View(profile);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(CandidateEditProfile profile)
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
        if (!ViewBag.IsAuth) return Redirect("/Login");

        string token = Request.Cookies["ActionLogin"]!;

        string email = GetEmail(token);
        Candidate candidate = (await _db.Candidates!.FirstOrDefaultAsync(c => c.Email == email))!;
        candidate.Name = profile.Name;
        candidate.Phone = profile.Phone;
        candidate.LastEducation = profile.LastEducation;

        await _db.SaveChangesAsync();

        return Redirect("/Profile");
    }

    private string GetEmail(string token)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(
           _configuration.GetSection("AppSettings:TokenCandidate").Value!
        ));

        ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler()
            .ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out _);

        return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value!;
    }

    [HttpGet("/Jobs")]
    public IActionResult Jobs()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

        return View();
    }

    [HttpGet("/Apply")]
    public IActionResult ApplyJob()
    {
        ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

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
