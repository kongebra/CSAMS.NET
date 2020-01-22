using Core.Interfaces;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Commands.Handlers {
    public class PersonCommandHandler {
        private readonly IPersonRepository _repository;
        private readonly CommandStoreService _commandStoreService;

        public PersonCommandHandler(
            IPersonRepository repository,
            CommandStoreService commandStoreService) {
            _repository = repository;
            _commandStoreService = commandStoreService;
        }

        public async Task HandleAsync(CreatePersonCommand command) {
            try {
                var person = new Person {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email,
                    Phone = command.Phone,
                    DateOfBirth = command.DateOfBirth,
                };

                await _repository.Add(person);
                await _commandStoreService.PushAsync(command);
            } catch (Exception) {
                throw;
            }
        }
    }
}
