using Converto;
using CSAMS.Course.Exceptions;
using CSAMS.Course.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Queries.Handlers {
    public class UserQueryHandler {
        private readonly DbContext _context;

        public UserQueryHandler(DbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<UserInfo>> HandleAsync(GetAllUsersQuery _) {
            var users = await _context.Set<Models.User>().ToListAsync();

            return users.Select(user => {
                return convert(user);
            });
        }

        public async Task<UserInfo> HandleAsync(GetUserInfoQuery query) {
            var user = await _context.Set<Models.User>()
                .FirstOrDefaultAsync(o => o.Username.Equals(query.Username, StringComparison.InvariantCultureIgnoreCase));

            if (user == null) {
                throw new UserNotFoundException($"Cannot find course '{query.Username}'");
            }

            return convert(user);
        }

        public async Task<UserInfo> HandleAsync(GetUserByPrivateEmailQuery query) {
            var user = await _context.Set<Models.User>()
                .FirstOrDefaultAsync(o => o.PrivateEmail.Equals(query.PrivateEmail, StringComparison.InvariantCultureIgnoreCase));

            if (user == null) {
                throw new UserNotFoundException($"Cannot find course '{query.PrivateEmail}'");
            }

            return convert(user);
        }

        private UserInfo convert(Models.User user) {
            return new UserInfo {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PrivateEmail = user.PrivateEmail,
                CreatedAt = user.CreatedAt,
                Password = user.Password,
            };
        }
    }
}
