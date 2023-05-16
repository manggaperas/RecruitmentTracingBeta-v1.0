using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using log4net;

using RecruitmentTracking.Models;
using IndexDb;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace RecruitmentTracking.Controllers;

public class AuthenticatorController : Controller
{
    private readonly DataContex _db = new();
    private readonly ILog _log;
    private readonly IConfiguration _configuration;

    public AuthenticatorController(IConfiguration configuration)
    {
        _log = LogManager.GetLogger(typeof(AuthenticatorController));
        _configuration = configuration;
    }

    [HttpGet("/Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("/Signup")]
    public IActionResult Signup()
    {
        return View();
    }

    // [HttpPost("/signup")]
    // public async Task<IActionResult> Signup(Candidate objCandidate)
    // {
    //     _db.Candidates!.Add(objCandidate);
    //     await _db.SaveChangesAsync();

    //     return Ok(objCandidate);
    // }

    [HttpPost]
    public async Task<IActionResult> Signups(CandidateSignup request)
    {
        if (_db.Candidates!.Any(a => a.Email == request.Email))
            return BadRequest("Email is already in use.");

        string HassedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        Candidate objCandidate = new()
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = HassedPassword,
        };

        _db.Candidates!.Add(objCandidate);
        await _db.SaveChangesAsync();

        return Ok(objCandidate);
    }

    [HttpPost]
    public async Task<IActionResult> Logins(LoginRequest request)
    {
        Admin objAdmin = (await _db.Admins!.FirstOrDefaultAsync(a => a.Email == request.Email))!;
        Candidate objCandidate = (await _db.Candidates!.FirstOrDefaultAsync(a => a.Email == request.Email))!;

        if (objAdmin != null && BCrypt.Net.BCrypt.Verify(request.Password, objAdmin.Password))
        {
            string token = CreateTokenAdmin(objAdmin!);
            return Ok(token);
        }

        if (objCandidate != null && BCrypt.Net.BCrypt.Verify(request.Password, objCandidate.PasswordHash))
        {
            string token = CreateTokenCandidate(objCandidate!);
            return Ok(token);
        }

        return BadRequest("User not found.");
    }

    private string CreateTokenAdmin(Admin objAdmin)
    {
        List<Claim> claims = new(){
            new Claim(ClaimTypes.Name, objAdmin.Email!),
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:TokenAdmin").Value!
        ));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken token = new(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string CreateTokenCandidate(Candidate objCandidate)
    {
        List<Claim> claims = new(){
            new Claim(ClaimTypes.Name, objCandidate.Email!),
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:TokenCandidate").Value!
        ));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken token = new(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
