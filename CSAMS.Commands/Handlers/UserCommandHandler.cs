using AutoMapper;
using CSAMS.Contracts.Interfaces;
using CSAMS.Core.Enums;
using CSAMS.Domain.Models;
using CSAMS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.Commands.Handlers {
    public class UserCommandHandler {
        private readonly IUserRepository _repository;
        private readonly CommandStoreService _commandStoreService;
        private readonly IMapper _mapper;

        public UserCommandHandler(
            IUserRepository repository, 
            CommandStoreService commandStoreService, 
            IMapper mapper) {
            _repository = repository;
            _commandStoreService = commandStoreService;
            _mapper = mapper;
        }

        public async Task HandleAsync(RegisterUserCommand command) {
            var user = _mapper.Map<User>(command);
            user.Role = UserRole.Student;

            await _repository.Add(user);
            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(UpdateUserDetailCommand command) {
            var user = await _repository.GetByUsername(command.Username);

            if (user == null) {
                throw new Exception($"User with username '{command.Username}' Not Found");
            }

            if (!string.IsNullOrEmpty(command.FirstName)) {
                user.FirstName = command.FirstName;
            }

            if (!string.IsNullOrEmpty(command.LastName)) {
                user.LastName = command.LastName;
            }

            if (!string.IsNullOrEmpty(command.Email)) {
                user.Email = command.Email;
            }

            if (!string.IsNullOrEmpty(command.DisplayName)) {
                user.DisplayName = command.DisplayName;
            }

            if (!string.IsNullOrEmpty(command.Phone)) {
                user.Phone = command.Phone;
            }

            if (!string.IsNullOrEmpty(command.Address)) {
                user.Address = command.Address;
            }

            if (command.Sex != null) {
                user.Sex = command.Sex;
            }

            if (command.DateOfBirth != null) {
                user.DateOfBirth = command.DateOfBirth;
            }

            await _repository.Update(user);
            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(DeleteUserCommand command) {
            var user = await _repository.GetByUsername(command.Username);

            if (user == null) {
                throw new Exception($"User with username '{command.Username}' Not Found");
            }

            await _repository.Remove(user);
            await _commandStoreService.PushAsync(command);
        }

        public async Task HandleAsync(VerifyEmailCommand command) {
            var user = await _repository.GetByEmail(command.Email);

            if (user == null) {
                throw new Exception($"User with email '{command.Email}' Not Found");
            }

            if (user.EmailVerified) {
                throw new Exception($"User has already verified email '{command.Email}'.");
            }

            user.EmailVerified = true;

            await _repository.Update(user);
            await _commandStoreService.PushAsync(command);
        }
    }
}
