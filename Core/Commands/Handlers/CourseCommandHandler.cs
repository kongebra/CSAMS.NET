using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Commands.Handlers {
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
                Code = command.Code.ToUpper(),
                Name = command.Name,
                Description = command.Description,
                Credits = command.Credits,
            };

            await _repository.Add(course);
            await _commandStoreService.PushAsync(command);
        }

    }
}
