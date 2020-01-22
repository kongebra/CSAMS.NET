using Converto;
using CSAMS.Course.Commands;
using CSAMS.Course.Commands.Handlers;
using CSAMS.Course.Exceptions;
using CSAMS.Course.Queries;
using CSAMS.Course.Queries.Handlers;
using CSAMS.Course.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CSAMS.Course.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller {
        private readonly CourseQueryHandler _queryHandler;
        private readonly CourseCommandHandler _commandHandler;

        public CourseController(
            CourseQueryHandler queryHandler,
            CourseCommandHandler commandHandler
            ) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllCoursesInfoQuery();
            var courses = await _queryHandler.HandleAsync(query);

            return Ok(courses);
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<ActionResult> Get(string code) {
            try {
                var query = new GetCourseInfoQuery(code);
                var course = await _queryHandler.HandleAsync(query);

                return Ok(course);
            } catch (CourseNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateCourseRequest request) {
            try {
                var command = request.ConvertTo<CreateCourseCommand>();
                await _commandHandler.HandleAsync(command);

                var query = new GetCourseInfoQuery(command.Code);
                var course = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { code = course.Code }, course);
            } catch (CourseAlreadyExistsException ex) {
                return Conflict(ex.Message);
            } catch (CourseNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] UpdateCourseRequest request) {
            try {
                var command = request.ConvertTo<UpdateCourseCommand>();
                await _commandHandler.HandleAsync(command);

                var query = new GetCourseInfoQuery(command.Code);
                var course = await _queryHandler.HandleAsync(query);

                return Ok(course);
            } catch (CourseNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{code}")]
        [Authorize]
        public async Task<ActionResult> Delete(string code) {
            try {
                var command = new DeleteCourseCommand(code);
                await _commandHandler.HandleAsync(command);

                return NoContent();
            } catch (CourseNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}