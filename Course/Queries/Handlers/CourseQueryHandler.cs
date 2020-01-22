using CSAMS.Course.Exceptions;
using CSAMS.Course.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Queries.Handlers {
    public class CourseQueryHandler {
        private readonly DbContext _context;

        public CourseQueryHandler(DbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<CourseInfo>> HandleAsync(GetAllCoursesInfoQuery _) {
            var courses = await _context.Set<Models.Course>().ToListAsync();

            return courses.Select(o => {
                return new CourseInfo {
                    Name = o.Name,
                    Code = o.Code,
                    Description = o.Description,
                };
            });
        }

        public async Task<CourseInfo> HandleAsync(GetCourseInfoQuery query) {
            var course = await _context.Set<Models.Course>()
                .FirstOrDefaultAsync(o => o.Code.Equals(query.Code, StringComparison.InvariantCultureIgnoreCase));

            if (course == null) {
                throw new CourseNotFoundException($"Cannot find course '{query.Code}'");
            }

            return new CourseInfo {
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
            };
        }
    }
}
