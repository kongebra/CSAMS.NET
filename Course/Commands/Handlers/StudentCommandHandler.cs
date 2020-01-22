using Converto;
using CSAMS.Course.Exceptions;
using CSAMS.Course.Models;
using CSAMS.Course.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CSAMS.Course.Commands.Handlers {

    public class StudentCommandHandler {
        private readonly DbContext _context;
        private readonly CommandStoreService _commandStoreService;
        private readonly AuthenticationService _authenticationService;

        public StudentCommandHandler(
            DbContext context,
            CommandStoreService commandStoreService,
            AuthenticationService authenticationService) {
            _context = context;
            _commandStoreService = commandStoreService;
            _authenticationService = authenticationService;
        }

        public async Task HandleAsync(RegisterStudentUserCommand command) {
            var user = await _context.Set<User>()
                .FirstOrDefaultAsync(o => o.Username.Equals(command.Username, StringComparison.InvariantCultureIgnoreCase));

            if (user == null) {
                throw new UserNotFoundException($"User with username '{command.Username}' not found");
            }

            var splittedEmail = user.Email.Split('@');
            var studentEmail = $"{splittedEmail[0]}@stud.{splittedEmail[1]}";

            var student = user.ConvertTo<Student>();
            student.Id = Guid.NewGuid();
            student.StudentEmail = studentEmail;

            await _context.AddAsync(student);
            await _context.SaveChangesAsync();

            await _commandStoreService.PushAsync(command);
        }
    }
}