using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using log4net;

using Server.Models;
using IndexDb;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexController : ControllerBase
{
    private readonly DataContex _db = new();
    private readonly ILog _log;
    private readonly IConfiguration _configuration;

    public IndexController(IConfiguration configuration)
    {
        _log = LogManager.GetLogger(typeof(IndexController));
        _configuration = configuration;
    }

    [HttpPost("/Signup")]
    public async Task<IActionResult> Signup(Candidate objCandidate)
    {
        _db.Candidates!.Add(objCandidate);
        await _db.SaveChangesAsync();

        return Ok(objCandidate);
    }

    [HttpPost("/Admin/Signup")]
    public async Task<IActionResult> AdminSignup(Admin request)
    {
        if (_db.Admins!.Any(a => a.Email == request.Email))
            return BadRequest("Email is already in use.");

        string HassedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        Admin objAdmin = new()
        {
            Name = request.Email,
            Email = request.Email,
            Password = HassedPassword,
        };

        _db.Admins!.Add(objAdmin);
        await _db.SaveChangesAsync();

        return Ok(objAdmin);
    }

    [HttpPost("/Admin/Login")]
    public async Task<IActionResult> AdminLogin(Admin request)
    {
        Admin objAdmin = (await _db.Admins!.FirstOrDefaultAsync(a => a.Email == request.Email))!;
        if (objAdmin == null)
            return BadRequest("User not found.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, objAdmin.Password))
            return BadRequest("Wrong password.");

        string token = CreateToken(objAdmin);

        return Ok(token);
    }

    private string CreateToken(Admin objAdmin)
    {
        List<Claim> claims = new(){
            new Claim(ClaimTypes.Name, objAdmin.Name!),
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
}
