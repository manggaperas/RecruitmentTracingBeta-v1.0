using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitmentTracking.Models;

namespace RecruitmentTracking.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }
    //public DbSet<User>? Users { get; set; } = default;
    public DbSet<Job>? Jobs { get; set; } = default;
    public DbSet<UserJob>? UserJobs { get; set; } = default;
    public DbSet<Candidate> Candidates { get; set; } = default;
}
