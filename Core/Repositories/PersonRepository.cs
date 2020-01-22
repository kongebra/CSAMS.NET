using Core.Interfaces;
using Core.Models;
using Core.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories {
    public class PersonRepository : IPersonRepository {
        private readonly DbContext _context;

        public PersonRepository(DbContext context) {
            _context = context;
        }

        public async Task Add(Person entity) {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = entity.CreatedAt;

            await _context.Set<Person>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAll() {
            return await _context.Set<Person>()
                .CountAsync();
        }

        public async Task<int> CountWhere(Expression<Func<Person, bool>> predicate) {
            return await _context.Set<Person>()
                .CountAsync(predicate);
        }

        public async Task<Person> FirstOrDefault(Expression<Func<Person, bool>> predicate) {
            return await _context.Set<Person>()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Person>> GetAll() {
            return await _context.Set<Person>()
                .ToListAsync();
        }

        public async Task<Person> GetById(Guid id) {
            return await _context.Set<Person>()
                .FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetWhere(Expression<Func<Person, bool>> predicate) {
            return await _context.Set<Person>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task Remove(Person entity) {
            _context.Set<Person>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Person entity) {
            entity.UpdatedAt = DateTime.Now;

            _context.Set<Person>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> Query(SearchForPersonQuery query) {
            var search = _context.Set<Person>().AsQueryable();

            if (!string.IsNullOrEmpty(query.FirstName)) {
                search = search.Where(p => p.FirstName.ToLower() == query.FirstName.ToLower());
            }

            if (!string.IsNullOrEmpty(query.LastName)) {
                search = search.Where(p => p.LastName.ToLower() == query.LastName.ToLower());
            }

            if (!string.IsNullOrEmpty(query.Email)) {
                search = search.Where(p => p.Email.ToLower() == query.Email.ToLower());
            }

            return await search.ToListAsync();
        }
    }
}
