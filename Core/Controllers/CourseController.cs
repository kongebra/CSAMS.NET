using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Commands;
using Core.Commands.Handlers;
using Core.Queries;
using Core.Queries.Handlers;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase {
        private readonly CourseCommandHandler _courseCommandHandler;
        private readonly CourseQueryHandler _courseQueryHandler;
        private readonly IMapper _mapper;

        public CourseController(
            CourseCommandHandler courseCommandHandler,
            CourseQueryHandler courseQueryHandler,
            IMapper mapper) {
            _courseCommandHandler = courseCommandHandler;
            _courseQueryHandler = courseQueryHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllCoursesQuery();
            var courses = await _courseQueryHandler.HandleAsync(query);

            return Ok(courses);
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<ActionResult> Get(string code) {
            var query = new GetCourseDetailQuery {
                Code = code,
            };
            var course = await _courseQueryHandler.HandleAsync(query);

            if (course == null) {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCourseRequest request) {
            var command = _mapper.Map<CreateCourseCommand>(request);
            await _courseCommandHandler.HandleAsync(command);

            var query = new GetCourseDetailQuery { Code = request.Code };
            var course = await _courseQueryHandler.HandleAsync(query);

            return CreatedAtAction(nameof(Get), new { code = course.Code }, course);
        }
    }
}