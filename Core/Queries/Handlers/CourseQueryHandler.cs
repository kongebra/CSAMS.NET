using Core.Interfaces;
using Core.Models;
using Core.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Queries.Handlers {
    public class CourseQueryHandler {
        private readonly ICourseRepository _repository;

        public CourseQueryHandler(ICourseRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<CourseInfo>> HandleAsync(GetAllCoursesQuery _) {
            var courses = await _repository.GetAll();

            return courses.Select(course => {
                return new CourseInfo {
                    Code = course.Code,
                    Name = course.Name,
                    Credits = course.Credits,
                };
            });
        }

        public async Task<CourseDetail> HandleAsync(GetCourseDetailQuery query) {
            var course = await _repository
                .FirstOrDefault(c => c.Code.Equals(query.Code, StringComparison.InvariantCultureIgnoreCase));

            return new CourseDetail {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Code = course.Code,
                Credits = course.Credits,
            };
        }
    }
}
