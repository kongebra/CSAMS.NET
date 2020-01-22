using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services {
    public class CommandStoreService {
        private readonly DbContext _context;

        public CommandStoreService(DbContext context) {
            _context = context;
        }

        public async Task PushAsync(object command) {
            await _context.Set<Command>()
                .AddAsync(new Command {
                    Type = command.GetType().Name,
                    Data = JsonSerializer.Serialize(command),
                    CreatedAt = DateTime.Now,
                });

            await _context.SaveChangesAsync();
        }
    }
}
