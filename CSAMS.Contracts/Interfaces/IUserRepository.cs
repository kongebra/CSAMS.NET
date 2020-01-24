using CSAMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.Contracts.Interfaces {
    public interface IUserRepository : IAsyncRepository<User> {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task<IEnumerable<User>> Search(dynamic query);
    }
}
