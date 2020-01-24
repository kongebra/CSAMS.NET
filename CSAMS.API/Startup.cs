using AutoMapper;
using CSAMS.Commands.Handlers;
using CSAMS.Contracts.Interfaces;
using CSAMS.DAL;
using CSAMS.DAL.Repositories;
using CSAMS.Queries.Handlers;
using CSAMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            // DbContext
            services.AddDbContext<DbContext, CSAMSDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();

            // Commands
            services.AddScoped<CourseCommandHandler>();
            services.AddScoped<UserCommandHandler>();

            // Queries
            services.AddScoped<CourseQueryHandler>();
            services.AddScoped<UserQueryHandler>();
            services.AddScoped<AssignmentQueryHandler>();

            // Services
            services.AddScoped<CommandStoreService>();

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Authentication
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });

            // Controllers
            services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });
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