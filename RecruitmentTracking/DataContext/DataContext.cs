using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using RecruitmentTracking.Models;

namespace IndexDb
{
    public class DataContex : DbContext
    {
        private const string _path = "./DataContext/Data.db";

        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Candidate>? Candidates { get; set; }
        public DbSet<Job>? Jobs { get; set; }
        public DbSet<CandidateJob>? CandidateJobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source= {_path}");
        }

        public static void CreateDb()
        {
            using DataContex context = new();
            if (!context.Database.EnsureCreated())
                SQLiteConnection.CreateFile(_path);
        }
    }
}