using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CSAMS.DAL {

    public class CSAMSDbContext : DbContext {
        public DbSet<Command> Commands { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }

        public CSAMSDbContext(DbContextOptions<CSAMSDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Unique Keys
            modelBuilder.Entity<Course>()
                .HasIndex(o => o.Code)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(o => new { o.Email, o.Username })
                .IsUnique();
        }
    }
}