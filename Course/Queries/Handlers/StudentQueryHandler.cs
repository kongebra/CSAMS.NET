using CSAMS.Course.Exceptions;
using CSAMS.Course.Models;
using CSAMS.Course.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Queries.Handlers {
    public class StudentQueryHandler {
        private readonly DbContext _context;

        public StudentQueryHandler(DbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<StudentInfo>> HandleAsync(GetAllStudentsQuery _) {
            var students = await _context.Set<Student>().ToListAsync();

            return students.Select(student => {
                return convert(student);
            });
        }

        public async Task<StudentInfo> HandleAsync(GetStudentByUsernameQuery query) {
            var student = await _context.Set<Student>()
                .FirstOrDefaultAsync(o => o.Username.Equals(query.Username, StringComparison.InvariantCultureIgnoreCase));

            if (student == null) {
                throw new UserNotFoundException($"Student with username '{query.Username}' not found");
            }

            return convert(student);
        }

        private StudentInfo convert(Student student) {
            return new StudentInfo {
                Username = student.Username,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
            };
        }
    }
}
