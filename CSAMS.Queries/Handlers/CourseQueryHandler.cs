using CSAMS.Contracts.Interfaces;
using CSAMS.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Queries.Handlers {
    public class CourseQueryHandler {
        private readonly ICourseRepository _repository;

        public CourseQueryHandler(ICourseRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<CourseInfo>> HandleAsync(GetAllCoursesQuery _) {
            var courses = await _repository.GetAll();

            return courses.Select(course => {
                return new CourseInfo {
                    Name = course.Name,
                    Code = course.Code,
                };
            });
        }

        public async Task<CourseDetail> HandleAsync(GetCourseDetailQuery query) {
            var course = await _repository.GetByCode(query.Code);

            if (course == null) {
                return null;
            }

            return new CourseDetail {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                Description = course.Descripion,
            };
        }
    }
}
