using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Commands.Handlers {
    public class TeacherCommandHandler {
        private readonly ITeacherRepository _repository;
        private readonly CommandStoreService _commandStoreService;

        public TeacherCommandHandler(
            ITeacherRepository repository,
            CommandStoreService commandStoreService) {
            _repository = repository;
            _commandStoreService = commandStoreService;
        }

        public async Task HandleAsync(CreateTeacherCommand command) {
            try {
                var teacher = new Teacher {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email,
                    Phone = command.Phone,
                    DateOfBirth = command.DateOfBirth,
                };

                await _repository.Add(teacher);
                await _commandStoreService.PushAsync(command);
            } catch (Exception) {
                throw;
            }
        }
    }
}
