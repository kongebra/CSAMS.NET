using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories {
    public class TeacherRepository : ITeacherRepository {
        private readonly DbContext _context;

        public TeacherRepository(DbContext context) {
            _context = context;
        }

        public async Task Add(Teacher entity) {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = entity.CreatedAt;

            await _context.Set<Teacher>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAll() {
            return await _context.Set<Teacher>()
                .CountAsync();
        }

        public async Task<int> CountWhere(Expression<Func<Teacher, bool>> predicate) {
            return await _context.Set<Teacher>()
                .CountAsync(predicate);
        }

        public async Task<Teacher> FirstOrDefault(Expression<Func<Teacher, bool>> predicate) {
            return await _context.Set<Teacher>()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Teacher>> GetAll() {
            return await _context.Set<Teacher>()
                .ToListAsync();
        }

        public async Task<Teacher> GetById(Guid id) {
            return await _context.Set<Teacher>()
                .FindAsync(id);
        }

        public async Task<IEnumerable<Teacher>> GetWhere(Expression<Func<Teacher, bool>> predicate) {
            return await _context.Set<Teacher>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task Remove(Teacher entity) {
            _context.Set<Teacher>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Teacher entity) {
            entity.UpdatedAt = DateTime.Now;

            _context.Set<Teacher>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
