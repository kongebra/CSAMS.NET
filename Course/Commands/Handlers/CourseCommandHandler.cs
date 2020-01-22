using CSAMS.Course.Exceptions;
using CSAMS.Course.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CSAMS.Course.Commands.Handlers {

    public class CourseCommandHandler {
        private readonly DbContext _context;
        private readonly CommandStoreService _commandStoreService;
        private readonly AuthenticationService _authenticationService;

        public CourseCommandHandler(
            DbContext context,
            CommandStoreService commandStoreService,
            AuthenticationService authenticationService) {
            _context = context;
            _commandStoreService = commandStoreService;
            _authenticationService = authenticationService;
        }

        public async Task HandleAsync(CreateCourseCommand command) {
            var exists = await _context.Set<Models.Course>()
                .FirstOrDefaultAsync(o => o.Code.Equals(command.Code, StringComparison.InvariantCultureIgnoreCase));

            if (exists != null) {
                throw new CourseAlreadyExistsException($"Course with code '{command.Code}' already exists");
            }

            var course = new Models.Course {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Code = command.Code.ToUpper(),
                Description = command.Description,
            };

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();

            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(UpdateCourseCommand command) {
            var course = await _context.Set<Models.Course>()
                .FirstOrDefaultAsync(o => o.Code.Equals(command.Code, StringComparison.InvariantCultureIgnoreCase));

            if (course == null) {
                throw new CourseNotFoundException($"Course not found with code '{command.Code}'");
            }

            if (command.Name != null) {
                course.Name = command.Name;
            }

            if (command.Description != null) {
                course.Description = command.Description;
            }

            _context.Update(course);
            await _context.SaveChangesAsync();

            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(DeleteCourseCommand command) {
            var course = await _context.Set<Models.Course>()
                .FirstOrDefaultAsync(o => o.Code.Equals(command.Code, StringComparison.InvariantCultureIgnoreCase));

            if (course == null) {
                throw new CourseNotFoundException($"Course not found with code '{command.Code}'");
            }

            _context.Remove(course);
            await _context.SaveChangesAsync();

            await _commandStoreService.PushAsync(command);
        }
    }
}