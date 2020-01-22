using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSAMS.Commands;
using CSAMS.Commands.Handlers;
using CSAMS.Contracts.Requests;
using CSAMS.Queries;
using CSAMS.Queries.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CSAMS.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/[controller]s")]
    public class CourseController : ControllerBase {
        private readonly CourseQueryHandler _queryHandler;
        private readonly CourseCommandHandler _commandHandler;

        public CourseController(CourseQueryHandler queryHandler, CourseCommandHandler commandHandler) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        } 
        
        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllCoursesQuery();
            var courses = await _queryHandler.HandleAsync(query);

            return Ok(courses);
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<ActionResult> Get(string code) {
            var query = new GetCourseDetailQuery { Code = code };
            var course = await _queryHandler.HandleAsync(query);

            if (course == null) {
                // TODO (Svein): Create a response body
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCourseRequest request) {
            try {
                // TODO (Svein): Add AutoMapper
                var command = new CreateCourseCommand {
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                };
                await _commandHandler.HandleAsync(command);

                var query = new GetCourseDetailQuery { Code = command.Code };
                var course = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { code = course.Code }, course);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{code}")]
        public async Task<ActionResult> Put([FromBody] UpdateCourseRequest request, string code) {
            try {
                // TODO (Svein): Add AutoMapper
                var command = new UpdateCourseCommand {
                    Code = code,
                    Name = request.Name,
                    Description = request.Description,
                };
                await _commandHandler.HandleAsync(command);

                return Ok();
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{code}")]
        public async Task<ActionResult> Delete(string code) {
            try {
                var command = new DeleteCourseCommand { Code = code };
                await _commandHandler.HandleAsync(command);

                return NoContent();
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}