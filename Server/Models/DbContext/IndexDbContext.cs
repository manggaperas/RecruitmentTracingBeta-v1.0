using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace IndexDbContex
{
    public class IndexDbContex : DbContext
    {
        private const string _path = "./Models/DbContext/Data.db";

        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Candidate>? Candidates { get; set; }
        public DbSet<Job>? Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source= Data Source= {_path}");
        }

        public static void CreateDb()
        {
            using IndexDbContex context = new();
            if (!context.Database.EnsureCreated())
                SQLiteConnection.CreateFile(_path);
        }
    }
}