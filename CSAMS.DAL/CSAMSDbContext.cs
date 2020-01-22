using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CSAMS.DAL {

    public class CSAMSDbContext : DbContext {
        DbSet<Course> Courses { get; set; }

        public CSAMSDbContext(DbContextOptions<CSAMSDbContext> options) : base(options) {
        }
    }
}