﻿using AutoMapper;
using CSAMS.Contracts.Interfaces;
using CSAMS.Contracts.Responses;
using CSAMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Queries.Handlers {
    public class CourseQueryHandler {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;

        public CourseQueryHandler(ICourseRepository repository,IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseInfo>> HandleAsync(GetAllCoursesQuery _) {
            var courses = await _repository.GetAll();

            return courses.Select(course => {
                return _mapper.Map<CourseInfo>(course);
            });
        }

        public async Task<CourseDetail> HandleAsync(GetCourseDetailQuery query) {
            var course = await _repository.GetByCode(query.Code);

            if (course == null) {
                throw new EntityNotFoundException($"Course with code '{query.Code}' Not Found.");
            }

            return _mapper.Map<CourseDetail>(course);
        }

        public async Task<CourseDetail> HandleAsync(GetCourseByIdQuery query) {
            var course = await _repository.GetById(query.Id);

            if (course == null) {
                throw new EntityNotFoundException($"Course with ID '{query.Id}' Not Found.");
            }

            return _mapper.Map<CourseDetail>(course);
        }

        public async Task<IEnumerable<AssignmentDetail>> HandleAsync(GetAssignmentsInCourseQuery query) {
            var course = await _repository.GetByCode(query.CourseCode);

            if (course == null) {
                throw new EntityNotFoundException($"Course with code '{query.CourseCode}' Not Found.");
            }

            return course.Assignments.Select(assignment => {
                return _mapper.Map<AssignmentDetail>(assignment);
            });
        }
    }
}
