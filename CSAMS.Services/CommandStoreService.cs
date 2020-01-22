using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSAMS.Services {
    public class CommandStoreService {
        private readonly DbContext _context;

        public CommandStoreService(DbContext context) {
            _context = context;
        }

        public async Task PushAsync(object command) {
            await _context.Set<Command>()
                .AddAsync(new Command {
                    // TODO (Svein): Implement authentication service for this
                    UserId = Guid.Empty,
                    Type = command.GetType().Name,
                    Data = JsonSerializer.Serialize(command),
                    CreatedAt = DateTime.Now,
                });

            await _context.SaveChangesAsync();
        }
    }
}
