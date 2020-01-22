using CSAMS.Course.Exceptions;
using CSAMS.Course.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CSAMS.Course.Commands.Handlers {

    public class UserCommandHandler {
        private readonly DbContext _context;
        private readonly CommandStoreService _commandStoreService;
        private readonly AuthenticationService _authenticationService;

        public UserCommandHandler(
            DbContext context,
            CommandStoreService commandStoreService,
            AuthenticationService authenticationService) {
            _context = context;
            _commandStoreService = commandStoreService;
            _authenticationService = authenticationService;
        }

        public async Task HandleAsync(CreateUserCommand command) {
            var exists = await _context.Set<Models.User>()
                .FirstOrDefaultAsync(o => o.PrivateEmail.Equals(command.Email, StringComparison.InvariantCultureIgnoreCase));

            if (exists != null) {
                throw new EmailAlreadyInUseException($"user with given private email ('{command.Email}') already exists");
            }

            if (command.FirstName == null
                && command.FirstName == ""
                && command.LastName == null
                && command.LastName == "") {
                throw new Exception($"First name or last name cannot be empty or blank.");
            }

            var username = await generateUsername(command.FirstName, command.LastName);
            var email = $"{username}@school.edu";

            var password = await generatePassword();
            var currentTime = DateTime.Now;
            var user = new Models.User {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Username = username,
                Email = email,
                PrivateEmail = command.Email,
                Password = password,
                CreatedAt = currentTime,
                UpdatedAt = currentTime,
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await _commandStoreService.PushAsync(command);
        }

        private async Task<string> generateUsername(string firstName, string lastName) {
            var filteredFirstName = filter(firstName);
            var filteredLastName = filter(lastName);

            var defaultStart = 2;
            var defaultEnd = 5;

            var fnStart = Math.Min(defaultStart, firstName.Length);
            var lnStart = Math.Min(defaultStart, lastName.Length);

            var fnEnd = Math.Min(defaultEnd, firstName.Length);
            var lnEnd = Math.Min(defaultEnd, lastName.Length);

            for (var j = lnStart; j <= lnEnd; j++) {
                for (var i = fnStart; i <= fnEnd; i++) {
                    var fn = filteredFirstName.Substring(0, i);
                    var ln = filteredLastName.Substring(0, j);
                    var username = $"{fn}{ln}";

                    var exists = await usernameExists(username);

                    if (!exists) {
                        return username;
                    }
                }
            }

            throw new Exception($"Could not create unique username for {firstName} {lastName}");
        }

        private string filter(string name) {
            return name.ToLower().Replace('ø', 'o').Replace('å', 'a').Replace("æ", "ae");
        }

        private async Task<bool> usernameExists(string username) {
            var exists = await _context.Set<Models.User>()
                .FirstOrDefaultAsync(o => o.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));

            return exists != null;
        }

        private async Task<string> generatePassword() {
            return await Task.FromResult("super-secret-password");
        }
    }
}