using CSAMS.Course.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSAMS.Course.Services {
    public class CommandStoreService {
        public readonly DbContext _context;
        public readonly AuthenticationService _authenticationService;

        public CommandStoreService(
            DbContext context,
            AuthenticationService authenticationService
            ) {
            _context = context;
            _authenticationService = authenticationService;
        }

        public async Task PushAsync(object command) {
            await _context.Set<Command>()
                .AddAsync(new Command {
                    Type = command.GetType().Name,
                    Data = JsonSerializer.Serialize(command),
                    CreatedAt = DateTime.Now,
                    UserId = _authenticationService.GetUserId(),
                });

            await _context.SaveChangesAsync();
        }
    }
}
