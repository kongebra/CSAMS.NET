using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Commands.Handlers;
using Core.Data;
using Core.Interfaces;
using Core.Models;
using Core.Queries.Handlers;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Database
            services.AddDbContext<DbContext, CoreContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Services
            services.AddScoped<CommandStoreService>();

            // Command handlers
            services.AddScoped<CourseCommandHandler>();
            services.AddScoped<PersonCommandHandler>();
            services.AddScoped<TeacherCommandHandler>();

            // Query handlers
            services.AddScoped<CourseQueryHandler>();
            services.AddScoped<PersonQueryHandler>();
            services.AddScoped<TeacherQueryHandler>();

            // Controllers
            services.AddControllers();

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            // app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
