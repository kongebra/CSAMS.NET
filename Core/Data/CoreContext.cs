using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Data {
    public class CoreContext : DbContext {
        public CoreContext(DbContextOptions<CoreContext> options) : base(options) { }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Many to many
            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseId, c.TeacherId });

            modelBuilder.Entity<OfficeAssignment>()
                .HasKey(o => new { o.OfficeId, o.TeacherId });

            // Unique keys
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Code)
                .IsUnique();
            
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<Teacher>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
