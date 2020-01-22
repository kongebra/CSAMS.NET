using CSAMS.Contracts.Interfaces;
using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSAMS.DAL.Repositories {

    public class CourseRepository : ICourseRepository {
        private readonly DbContext _context;

        public CourseRepository(DbContext context) {
            _context = context;
        }

        public async Task Add(Course entity) {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = entity.CreatedAt;

            await _context.Set<Course>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAll() {
            return await _context.Set<Course>().CountAsync();
        }

        public async Task<int> CountWhere(Expression<Func<Course, bool>> predicate) {
            return await _context.Set<Course>().CountAsync(predicate);
        }

        public async Task<Course> FirstOrDefault(Expression<Func<Course, bool>> predicate) {
            return await _context.Set<Course>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Course>> GetAll() {
            return await _context.Set<Course>().ToListAsync();
        }

        public async Task<Course> GetByCode(string code) {
            return await FirstOrDefault(o => o.Code.ToLower() == code.ToLower());
        }

        public async Task<Course> GetById(Guid id) {
            return await _context.Set<Course>().FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetWhere(Expression<Func<Course, bool>> predicate) {
            return await _context.Set<Course>().Where(predicate).ToListAsync();
        }

        public async Task Remove(Course entity) {
            _context.Set<Course>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Course entity) {
            entity.UpdatedAt = DateTime.Now;

            _context.Set<Course>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}