using Microsoft.EntityFrameworkCore;

namespace CSAMS.Course.Models {
    public class CourseContext : DbContext {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

        public DbSet<Command> CommandStore { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Teacher>().ToTable("Teachers");
        }
    }
}
