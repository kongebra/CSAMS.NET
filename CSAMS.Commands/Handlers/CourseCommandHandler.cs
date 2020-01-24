using CSAMS.Contracts.Interfaces;
using CSAMS.Domain.Models;
using CSAMS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.Commands.Handlers {
    public class CourseCommandHandler {
        private readonly ICourseRepository _repository;
        private readonly CommandStoreService _commandStoreService;

        public CourseCommandHandler(
            ICourseRepository repository, 
            CommandStoreService commandStoreService) {
            _repository = repository;
            _commandStoreService = commandStoreService;
        }

        public async Task HandleAsync(CreateCourseCommand command) {
            var course = new Course {
                Name = command.Name,
                Code = command.Code.ToUpper(),
                Description = command.Description,
            };

            await _repository.Add(course);
            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(UpdateCourseCommand command) {
            var course = await _repository.GetByCode(command.Code);

            if (course == null) {
                throw new Exception($"Course with code '{command.Code}' Not Found.");
            }

            if (!string.IsNullOrEmpty(command.Name)) {
                course.Name = command.Name;
            }

            if (!string.IsNullOrEmpty(command.Description)) {
                course.Description = command.Description;
            }

            await _repository.Update(course);
            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(DeleteCourseCommand command) {
            var course = await _repository.GetByCode(command.Code);

            if (course == null) {
                throw new Exception($"Course with code '{command.Code}' Not Found.");
            }

            await _repository.Remove(course);
            await _commandStoreService.PushAsync(command);
        }
    }
}
