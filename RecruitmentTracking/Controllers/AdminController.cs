using Microsoft.AspNetCore.Mvc;

using RecruitmentTracking.Models;
using RecruitmentTracking.Data;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentTracking.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _db = new();
    //private readonly ILog _log;
    private readonly IConfiguration _configuration;

    public AdminController(IConfiguration configuration)
    {
        //_log = LogManager.GetLogger(typeof(AdminController));
        _configuration = configuration;
    }

    // [HttpGet("/Admin")]
    // public async Task<IActionResult> Index()
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
    //     ViewBag.IsAdmin = "admin";

    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out _, out string adminName);

    //     ViewBag.AdminName = adminName;

    //     List<JobData> listJob = new();
    //     foreach (Job job in _db.Jobs!.Where(j => j.IsJobAvailable).ToList())
    //     {
    //         int candidateCount = _db.CandidateJobs!.Where(c => c.JobId == job.JobId).Count();

    //         JobData data = new()
    //         {
    //             JobId = job.JobId,
    //             JobTitle = job.JobTitle,
    //             JobDescription = job.JobDescription,
    //             JobRequirement = job.JobRequirement,
    //             Location = job.Location,
    //             JobPostedDate = job.JobPostedDate,
    //             JobExpiredDate = job.JobExpiredDate,
    //             CandidateCout = candidateCount,
    //         };
    //         listJob.Add(data);
    //     }

    //     return View(listJob);
    // }

    // // Admin/JobClosed
    // [HttpGet]
    // public async Task<IActionResult> JobClosed()
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
    //     ViewBag.IsAdmin = "admin";

    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out _, out string adminName);

    //     ViewBag.AdminName = adminName;

    //     List<JobData> listJob = new();
    //     foreach (Job job in _db.Jobs!.Where(j => !j.IsJobAvailable).ToList())
    //     {
    //         int candidateCout = _db.CandidateJobs!.Where(c => c.JobId == job.JobId).Count();
    //         JobData data = new()
    //         {
    //             JobId = job.JobId,
    //             JobTitle = job.JobTitle,
    //             JobDescription = job.JobDescription,
    //             JobRequirement = job.JobRequirement,
    //             Location = job.Location,
    //             JobPostedDate = job.JobPostedDate,
    //             JobExpiredDate = job.JobExpiredDate,
    //             CandidateCout = candidateCout,
    //         };
    //         listJob.Add(data);
    //     }

    //     return View(listJob);
    // }

    // [HttpPost]
    // public async Task<IActionResult> CloseTheJob(int id)
    // {
    //     Job objJob = _db.Jobs!.Find(id)!;

    //     objJob.IsJobAvailable = false;
    //     await _db.SaveChangesAsync();

    //     TempData["success"] = "Successfully Close a Job";
    //     return Redirect("/Admin");
    // }

    // [HttpPost]
    // public async Task<IActionResult> ActivateTheJob(int id)
    // {
    //     Job objJob = _db.Jobs!.Find(id)!;

    //     objJob.IsJobAvailable = true;
    //     await _db.SaveChangesAsync();

    //     TempData["success"] = "Successfully Activate a Job";
    //     return Redirect("/Admin/JobClosed");
    // }

    // // Add Feature, if candidate apply job > 0, job can't be closed
    // [HttpPost]
    // public async Task<IActionResult> DeleteJob(int id)
    // {
    //     if (_db.CandidateJobs!.Where(cj => cj.JobId == id).Any())
    //     {
    //         TempData["warning"] = "Job can't be closed because there are candidates who have applied for this job.";
    //         //return Redirect("/Admin");
    //         return Redirect("/Admin/JobClosed");
    //     }

    //     Job objJob = _db.Jobs!.Find(id)!;
    //     _db.Jobs.Remove(objJob);
    //     await _db.SaveChangesAsync();

    //     TempData["success"] = "Successfully Delete a Job";
    //     return Redirect("/Admin/JobClosed");
    // }

    // [HttpGet]
    // public IActionResult CreateJob()
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
    //     ViewBag.IsAdmin = "admin";

    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out _, out string adminName);

    //     ViewBag.AdminName = adminName;

    //     return View();
    // }

    // [HttpPost]
    // public async Task<IActionResult> CreateJobs(JobCreate objJob)
    // {
    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out string email, out _);

    //     Admin admin = _db.Admins!.FirstOrDefault(a => a.Email == email)!;
    //     Job newJob = new()
    //     {
    //         JobTitle = objJob.JobTitle,
    //         JobDescription = objJob.JobDescription,
    //         JobExpiredDate = objJob.JobExpiredDate,
    //         JobRequirement = objJob.JobRequirement!.Replace("\r\n", "\n"),
    //         JobPostedDate = DateTime.Now,
    //         Location = objJob.Location,
    //         IsJobAvailable = true,
    //         Admin = admin,
    //     };
    //     _db.Jobs!.Add(newJob);
    //     await _db.SaveChangesAsync();

    //     _log.Info("Job Added.");

    //     TempData["success"] = "Successfully Created a Job";
    //     return Redirect("/Admin");
    // }

    // [HttpGet]
    // public IActionResult EditJob(int id)
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
    //     ViewBag.IsAdmin = "admin";

    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out _, out string adminName);

    //     ViewBag.AdminName = adminName;

    //     Job objJob = _db.Jobs!.FirstOrDefault(j => j.JobId == id)!;

    //     JobData data = new()
    //     {
    //         JobId = objJob.JobId,
    //         JobTitle = objJob.JobTitle,
    //         JobDescription = objJob.JobDescription,
    //         JobRequirement = objJob.JobRequirement!.Replace("\r\n", "\n"),
    //         Location = objJob.Location,
    //         JobPostedDate = objJob.JobPostedDate,
    //         JobExpiredDate = objJob.JobExpiredDate,
    //     };

    //     return View(data);
    // }

    // [HttpPost]
    // public async Task<IActionResult> EditJobs(JobCreate objJob)
    // {
    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out string email, out _);

    //     Job updateJob = _db.Jobs!.FirstOrDefault(j => j.JobId == objJob.JobId)!;
    //     updateJob.JobTitle = objJob.JobTitle;
    //     updateJob.JobDescription = objJob.JobDescription;
    //     updateJob.JobExpiredDate = objJob.JobExpiredDate;
    //     updateJob.JobRequirement = objJob.JobRequirement;
    //     updateJob.Location = objJob.Location;

    //     await _db.SaveChangesAsync();
    //     _log.Info("Job Updated.");

    //     TempData["success"] = "Successfully Edit a Job";
    //     return Redirect("/Admin");
    // }

    // [HttpGet]
    // public async Task<IActionResult> Administration(int id)
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
    //     ViewBag.IsAdmin = "admin";

    //     string token = Request.Cookies["ActionLogin"]!;
    //     GetDataAdmin(token, out _, out string name);

    //     ViewBag.AdminName = name;
    //     ViewBag.JobId = id;

    //     Job objJob = _db.Jobs!.FirstOrDefault(j => j.JobId == id)!;
    //     ViewBag.JobTitle = objJob.JobTitle;

    //     List<Candidate> listCandidates = _db.CandidateJobs!
    //                                     .Where(cj => cj.JobId == id)
    //                                     .Select(cj => cj.Candidate)
    //                                     .ToList()!;

    //     List<DataCandidateJobs> listDataCandidates = new();
    //     foreach (Candidate candidate in listCandidates)
    //     {
    //         if (candidate.StatusInJob == "Administration")
    //         {
    //             CandidateJob cj = _db.CandidateJobs!.FirstOrDefault(cj => cj.CandidateId == candidate.CandidateId)!;
    //             DataCandidateJobs dataCandidate = new()
    //             {
    //                 CandidateId = candidate.CandidateId,
    //                 Name = candidate.Name,
    //                 Email = candidate.Email,
    //                 LastEducation = candidate.LastEducation,
    //                 GPA = candidate.GPA,
    //                 CV = cj.CV,
    //             };
    //             listDataCandidates.Add(dataCandidate);
    //         }
    //     }

    //     return View(listDataCandidates);
    // }

    // [HttpPost]
    // public async Task<IActionResult> DownloadCV(int CandidateId, int JobId)
    // {
    //     CandidateJob CJ = _db.CandidateJobs!
    //                         .FirstOrDefault(cj => cj.JobId == JobId && cj.CandidateId == CandidateId)!;

    //     string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataCV", CJ.CV!);

    //     return PhysicalFile(filePath, "application/force-download", Path.GetFileName(filePath));
    // }

    // [HttpPost]
    // public async Task<IActionResult> Accept(int CandidateId, int JobId)
    // {

    //     Candidate objCandidate = _db.Candidates!.Find(CandidateId)!;
    //     int statusInJob = (int)Enum.Parse(typeof(ProcessType), objCandidate.StatusInJob!);

    //     //Console.WriteLine($"{statusInJob}");
    //     statusInJob++;
    //     objCandidate.StatusInJob = $"{(ProcessType)statusInJob}";

    //     //Console.WriteLine(objCandidate.StatusInJob);

    //     await _db.SaveChangesAsync();

    //     TempData["success"] = "Candidate Accepted";
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

    // [HttpGet]
    // public IActionResult HRInterview()
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;
    //     ViewBag.IsAdmin = "admin";

    //     return View();
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


    // [HttpGet]
    // public IActionResult UserInterview()
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

    //     return View();
    // }

    // [HttpGet]
    // public IActionResult Offering()
    // {
    //     ViewBag.IsAuth = Request.Cookies["ActionLogin"]! != null;

    //     return View();
    // }




    // private void GetDataAdmin(string token, out string email, out string name)
    // {
    //     ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler()
    //         .ValidateToken(token, new TokenValidationParameters
    //         {
    //             ValidateIssuerSigningKey = true,
    //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
    //                                 _configuration.GetSection("AppSettings:TokenAdmin").Value!
    //                                 )),
    //             ValidateIssuer = false,
    //             ValidateAudience = false,
    //         }, out _);

    //     email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value!;
    //     name = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value!;
    // }
}
