using Microsoft.AspNetCore.Mvc;

using RecruitmentTracking.Models;
using RecruitmentTracking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentTracking.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AdminController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context, UserManager<User> userManager)
    {
        _logger = logger;
        _configuration = configuration;
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Job> listObjJob = await _context.Jobs!.Where(j => j.IsJobAvailable).ToListAsync();

        List<JobViewModel> listJobModel = new();
        foreach (Job job in listObjJob)
        {
            int candidateCount = _context.UserJobs!.Where(c => c.JobId == job.JobId).Count();

            JobViewModel modelView = new()
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                JobDescription = job.JobDescription,
                JobRequirement = job.JobRequirement,
                Location = job.Location,
                JobPostedDate = job.JobPostedDate,
                JobExpiredDate = job.JobExpiredDate,
                CandidateCout = candidateCount,
            };
            listJobModel.Add(modelView);
        }

        return View(listJobModel);
    }

    // Admin/JobClosed
    [HttpGet]
    public async Task<IActionResult> JobClosed()
    {
        User user = (await _userManager.GetUserAsync(User))!;

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        List<JobViewModel> listJob = new();
        foreach (Job job in _context.Jobs!.Where(j => !j.IsJobAvailable).ToList())
        {
            int candidateCout = _context.UserJobs!.Where(c => c.JobId == job.JobId).Count();
            JobViewModel data = new()
            {
                JobId = job.JobId,
                JobTitle = job.JobTitle,
                JobDescription = job.JobDescription,
                JobRequirement = job.JobRequirement,
                Location = job.Location,
                JobPostedDate = job.JobPostedDate,
                JobExpiredDate = job.JobExpiredDate,
                CandidateCout = candidateCout,
            };
            listJob.Add(data);
        }
        return View(listJob);
    }

    [HttpPost]
    public async Task<IActionResult> CloseTheJob(int id)
    {
        Job objJob = (await _context.Jobs!.FindAsync(id))!;

        objJob.IsJobAvailable = false;
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully Close a Job";
        return Redirect("/Admin");
    }

    [HttpPost]
    public async Task<IActionResult> ActivateTheJob(int id)
    {
        Job objJob = _context.Jobs!.Find(id)!;

        objJob.IsJobAvailable = true;
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully Activate a Job";
        return Redirect("/Admin/JobClosed");
    }

    // Add Feature, if candidate apply job > 0, job can't be closed
    [HttpPost]
    public async Task<IActionResult> DeleteJob(int id)
    {
        if ((await _context.UserJobs!.Where(cj => cj.JobId == id).AnyAsync())!)
        {
            TempData["warning"] = "Job can't be closed because there are candidates who have applied for this job.";
            //return Redirect("/Admin");
            return Redirect("/Admin/JobClosed");
        }

        Job objJob = _context.Jobs!.Find(id)!;
        _context.Jobs.Remove(objJob);
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully Delete a Job";
        return Redirect("/Admin/JobClosed");
    }

    //UI added
    [HttpGet]
    public async Task<IActionResult> CreateJob()
    {
        User user = (await _userManager.GetUserAsync(User))!;

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        ViewBag.AdminName = user.Name;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateJobs(JobCreate objJob)
    {
        User user = (await _userManager.GetUserAsync(User))!;

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        Job newJob = new()
        {
            JobTitle = objJob.JobTitle,
            JobDescription = objJob.JobDescription,
            JobExpiredDate = objJob.JobExpiredDate,
            JobRequirement = objJob.JobRequirement!.Replace("\r\n", "\n"),
            JobPostedDate = DateTime.Now,
            Location = objJob.Location,
            IsJobAvailable = true,
            User = user,
        };
        _context.Jobs!.Add(newJob);
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully Created a Job";
        return Redirect("/Admin");
    }

    [HttpGet]
    public async Task<IActionResult> EditJob(int id)
    {
        User user = (await _userManager.GetUserAsync(User))!;

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        ViewBag.AdminName = user.Name;

        Job objJob = (await _context.Jobs!.FindAsync(id))!;

        JobViewModel data = new()
        {
            JobId = objJob.JobId,
            JobTitle = objJob.JobTitle,
            JobDescription = objJob.JobDescription,
            JobRequirement = objJob.JobRequirement!.Replace("\r\n", "\n"),
            Location = objJob.Location,
            JobPostedDate = objJob.JobPostedDate,
            JobExpiredDate = objJob.JobExpiredDate,
        };

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> EditJobs(JobCreate objJob)
    {
        User user = (await _userManager.GetUserAsync(User))!;

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        Job updateJob = (await _context.Jobs!.FindAsync(objJob.JobId))!;
        updateJob.JobTitle = objJob.JobTitle;
        updateJob.JobDescription = objJob.JobDescription;
        updateJob.JobExpiredDate = objJob.JobExpiredDate;
        updateJob.JobRequirement = objJob.JobRequirement;
        updateJob.Location = objJob.Location;

        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully Edit a Job";
        return Redirect("/Admin");
    }

    [HttpGet]
    public async Task<IActionResult> RecruitmentProcess(int id)
    {
        User user = (await _userManager.GetUserAsync(User))!;

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        ViewBag.AdminName = user.Name;

        Job objJob = (await _context.Jobs!.FindAsync(id))!;
        ViewBag.JobTitle = objJob.JobTitle;

        List<User> listCandidates = (await _context.UserJobs!
                                        .Where(cj => cj.JobId == id)
                                        .Select(cj => cj.User)
                                        .ToListAsync())!;

        List<DataCandidateJobs> listDataCandidates = new();
        foreach (User candidate in listCandidates)
        {
            string statusInJob = (await _context.UserJobs!.FirstOrDefaultAsync(uj => uj.JobId == id && uj.UserId == user.Id))!.StatusInJob!;
            if (statusInJob == "Administration")
            {
                UserJob cj = _context.UserJobs!.FirstOrDefault(cj => cj.UserId == user.Id)!;
                Candidate objCandidate = (await _context.Candidates.FirstOrDefaultAsync(c => c.UserId == user.Id))!;
                DataCandidateJobs dataCandidate = new()
                {
                    CandidateId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    LastEducation = objCandidate.LastEducation,
                    GPA = objCandidate.GPA,
                    CV = cj.CV,
                };
                listDataCandidates.Add(dataCandidate);
            }
        }
        return View(listDataCandidates);
    }

    [HttpPost]
    public async Task<IActionResult> DownloadCV(string UserId, int JobId)
    {
        UserJob CJ = (await _context.UserJobs!
                            .FirstOrDefaultAsync(cj => cj.JobId == JobId && cj.UserId == UserId))!;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataCV", CJ.CV!);

        return PhysicalFile(filePath, "application/force-download", Path.GetFileName(filePath));
    }

    // [HttpPost]
    // public async Task<IActionResult> Accept(int CandidateId, int JobId)
    // {
    //     Candidate objCandidate = _context.Candidates!.Find(CandidateId)!;
    //     int statusInJob = (int)Enum.Parse(typeof(ProcessType), objCandidate.StatusInJob!);
    //     statusInJob++;
    //     objCandidate.StatusInJob = $"{(ProcessType)statusInJob}";

    //     await _context.SaveChangesAsync();

    //     TempData["success"] = "Candidate Accepted";
    //     return Redirect($"/Admin/Administration/{JobId}");
    // }

    // [HttpPost]
    // public async Task<IActionResult> Rejected(int CandidateId, int JobId)
    // {
    //     Candidate objCandidate = _db.Candidates!.Find(CandidateId)!;

    //     objCandidate.StatusInJob = $"{ProcessType.Rejected}";

    //     await _db.SaveChangesAsync();

    //     TempData["success"] = "Candidate Rejected";
    //     return Redirect($"/Admin/Administration/{JobId}");
    // }

    // [HttpPost]
    // public async Task<IActionResult> SendEmailHRInterview(int JobId, int CandidateId)
    // {
    //     Job objJob = (await _db.Jobs!.FindAsync(JobId))!;
    //     string bodyEmail = objJob.EmailHR!;

    //     return default;
    // }

    // [HttpPost]
    // public async Task<IActionResult> TemplateEmail(EmailTemplate email)
    // {
    //     Job objJob = (await _db.Jobs!.FindAsync(email.JobId))!;
    //     objJob.EmailHR = email.EmailHR;
    //     await _db.SaveChangesAsync();

    //     return View(email);
    // }

}
