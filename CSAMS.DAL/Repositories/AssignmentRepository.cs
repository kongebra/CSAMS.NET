using CSAMS.Contracts.Interfaces;
using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.DAL.Repositories {
    public class AssignmentRepository : IAssignmentRepository {
        private readonly DbContext _context;
        
        public AssignmentRepository(DbContext context) {
            _context = context;
        }

        public async Task Add(Assignment entity) {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = entity.CreatedAt;

            await _context.Set<Assignment>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAll() {
            return await _context.Set<Assignment>().CountAsync();
        }

        public async Task<int> CountWhere(Expression<Func<Assignment, bool>> predicate) {
            return await _context.Set<Assignment>().CountAsync(predicate);
        }

        public async Task<Assignment> FirstOrDefault(Expression<Func<Assignment, bool>> predicate) {
            return await _context.Set<Assignment>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Assignment>> GetAll() {
            return await _context.Set<Assignment>().ToListAsync();
        }

        public async Task<Assignment> GetById(Guid id) {
            return await _context.Set<Assignment>().FindAsync(id);
        }

        public async Task<IEnumerable<Assignment>> GetWhere(Expression<Func<Assignment, bool>> predicate) {
            return await _context.Set<Assignment>().Where(predicate).ToListAsync();
        }

        public async Task Remove(Assignment entity) {
            _context.Set<Assignment>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Assignment entity) {
            entity.UpdatedAt = DateTime.Now;

            _context.Set<Assignment>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
