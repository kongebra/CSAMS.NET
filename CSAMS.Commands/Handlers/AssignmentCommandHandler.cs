using CSAMS.Contracts.Interfaces;
using CSAMS.Core.Exceptions;
using CSAMS.Domain.Models;
using CSAMS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.Commands.Handlers {
    public class AssignmentCommandHandler {
        private readonly IAssignmentRepository _repository;
        private readonly CommandStoreService _commandStoreService;

        public AssignmentCommandHandler(IAssignmentRepository repository, CommandStoreService commandStoreService) {
            _repository = repository;
            _commandStoreService = commandStoreService;
        }

        public async Task<Guid> HandleAsync(CreateAssignmentCommand command) {
            var assignment = new Assignment {
                Title = command.Title,
                Description = command.Description,
                PublishAt = command.PublishAt,
                DeadlineAt = command.DeadlineAt,
                CourseId = command.CourseId.GetValueOrDefault(),
            };

            await _repository.Add(assignment);
            await _commandStoreService.PushAsync(command);

            return assignment.Id;
        }

        public async Task HandleAsync(UpdateAssignmentCommand command) {
            var assignment = await _repository.GetById(command.Id);

            if (assignment == null) {
                throw new EntityNotFoundException($"Assignment with ID '{command.Id}' Not Found.");
            }

            if (!string.IsNullOrEmpty(command.Title)) {
                assignment.Title = command.Title;
            }

            if (!string.IsNullOrEmpty(command.Description)) {
                assignment.Description = command.Description;
            }

            if (command.PublishAt != null) {
                assignment.PublishAt = command.PublishAt;
            }

            if (command.DeadlineAt != null) {
                assignment.DeadlineAt = command.DeadlineAt;
            }

            await _repository.Update(assignment);
            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(DeleteAssignmentCommand command) {
            var assignment = await _repository.GetById(command.Id);

            if (assignment == null) {
                throw new EntityNotFoundException($"Assignment with ID '{command.Id}' Not Found.");
            }

            await _repository.Remove(assignment);
            await _commandStoreService.PushAsync(command);
        }
    }
}
