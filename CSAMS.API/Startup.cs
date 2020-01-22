using CSAMS.Commands.Handlers;
using CSAMS.Contracts.Interfaces;
using CSAMS.DAL;
using CSAMS.DAL.Repositories;
using CSAMS.Queries.Handlers;
using CSAMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSAMS.API {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // DbContext's
            services.AddDbContext<DbContext, CSAMSDbContext>(options => {
                options.UseInMemoryDatabase("CSAMS");
                // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();

            // Commands
            services.AddScoped<CourseCommandHandler>();

            // Queries
            services.AddScoped<CourseQueryHandler>();

            // Services
            services.AddScoped<CommandStoreService>();

            // Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}