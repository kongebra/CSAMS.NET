using CSAMS.Course.Commands.Handlers;
using CSAMS.Course.Models;
using CSAMS.Course.Queries.Handlers;
using CSAMS.Course.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSAMS.Course {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<DbContext, CourseContext>(options => {
                options.UseInMemoryDatabase("CSAMSDatabase");
            });

            // Services
            services.AddScoped<AuthenticationService>();
            services.AddScoped<CommandStoreService>();

            // Command handlers
            services.AddScoped<CourseCommandHandler>();
            services.AddScoped<UserCommandHandler>();
            services.AddScoped<StudentCommandHandler>();

            // Query handlers
            services.AddScoped<CourseQueryHandler>();
            services.AddScoped<UserQueryHandler>();
            services.AddScoped<StudentQueryHandler>();

            // Authentication
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
