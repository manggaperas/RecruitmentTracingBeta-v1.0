using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTracking.Models;

namespace RecruitmentTracking.Controllers;

public class CandidateController : Controller
{
    private readonly ILogger<CandidateController> _logger;

    public CandidateController(ILogger<CandidateController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
