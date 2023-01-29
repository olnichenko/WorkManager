using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal
{
    public class WorkManagerDbContext : DbContext
    {
        private string _connectionString;
        public WorkManagerDbContext(string connectionString)
        {
            _connectionString = connectionString;
            //Database.EnsureCreated();
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
                //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<TimeSpent> TimeSpents { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Version> Versions { get; set; }
        public DbSet<ProjectsToUsers> ProjectsToUsers { get; set; }
    }
}
