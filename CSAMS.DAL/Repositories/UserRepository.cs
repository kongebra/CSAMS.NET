using CSAMS.Contracts.Interfaces;
using CSAMS.Core.Enums;
using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.DAL.Repositories {
    public class UserRepository : IUserRepository {
        private readonly DbContext _context;

        public UserRepository(DbContext context) {
            _context = context;
        }

        public async Task Add(User entity) {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = entity.CreatedAt;

            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAll() {
            return await _context.Set<User>().CountAsync();
        }

        public async Task<int> CountWhere(Expression<Func<User, bool>> predicate) {
            return await _context.Set<User>().CountAsync(predicate);
        }

        public async Task<User> FirstOrDefault(Expression<Func<User, bool>> predicate) {
            return await _context.Set<User>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<User>> GetAll() {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetByEmail(string email) {
            return await FirstOrDefault(o => o.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetById(Guid id) {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<User> GetByUsername(string username) {
            return await FirstOrDefault(o => o.Username.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<User>> GetWhere(Expression<Func<User, bool>> predicate) {
            return await _context.Set<User>().Where(predicate).ToListAsync();
        }

        public async Task Remove(User entity) {
            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> Search(dynamic query) {
            var users = _context.Set<User>().AsQueryable();

            if (!string.IsNullOrEmpty(query?.FirstName)) {
                var fn = query?.FirstName as string;
                users = users.Where(o => o.FirstName.ToLower() == fn.ToLower());
            }

            if (!string.IsNullOrEmpty(query?.LastName)) {
                var ln = query?.LastName as string;
                users = users.Where(o => o.LastName.ToLower() == ln.ToLower());
            }

            if (!string.IsNullOrEmpty(query?.Email)) {
                var email = query?.Email as string;
                users = users.Where(o => o.Email.ToLower() == email.ToLower());
            }

            if (!string.IsNullOrEmpty(query?.Username)) {
                var username = query?.Username as string;
                users = users.Where(o => o.Username.ToLower() == username.ToLower());
            }

            if (query?.DateOfBirth != null) {
                var dob = (DateTime)query?.DateOfBirth;
                users = users.Where(o => o.DateOfBirth.HasValue 
                                    ? o.DateOfBirth.Value.Year == dob.Year
                                        && o.DateOfBirth.Value.Month == dob.Month
                                        && o.DateOfBirth.Value.Day == dob.Day
                                    : false);
            }

            if (query?.Sex != null) {
                var sex = (Sex)query?.sex;
                users = users.Where(o => o.Sex == sex);
            }

            return await users.ToListAsync();
        }

        public async Task Update(User entity) {
            entity.UpdatedAt = DateTime.Now;

            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
