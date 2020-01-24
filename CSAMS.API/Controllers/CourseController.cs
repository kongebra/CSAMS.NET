using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CSAMS.Commands;
using CSAMS.Commands.Handlers;
using CSAMS.Contracts.Requests;
using CSAMS.Core.Exceptions;
using CSAMS.Queries;
using CSAMS.Queries.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSAMS.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/[controller]s")]
    public class CourseController : ControllerBase {
        private readonly CourseQueryHandler _queryHandler;
        private readonly CourseCommandHandler _commandHandler;
        private readonly IMapper _mapper;

        public CourseController(
            CourseQueryHandler queryHandler, 
            CourseCommandHandler commandHandler,
            IMapper mapper) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _mapper = mapper;
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
            try {
                var query = new GetCourseDetailQuery { Code = code };
                var course = await _queryHandler.HandleAsync(query);

                return Ok(course);
            } catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCourseRequest request) {
            try {
                var command = _mapper.Map<CreateCourseCommand>(request);
                await _commandHandler.HandleAsync(command);

                var query = new GetCourseDetailQuery { Code = command.Code };
                var course = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { code = course.Code }, course);
            } catch (Exception ex) {
                // Check if exception is cause of duplicated key
                if (ex.InnerException.Message.Contains("duplicate key")) {
                    return BadRequest(new {
                        title = $"Course with code '{request.Code.ToUpper()}' already exists.",
                        status = HttpStatusCode.BadRequest,
                    });
                }

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{code}")]
        public async Task<ActionResult> Put([FromBody] UpdateCourseRequest request, string code) {
            try {
                var command = _mapper.Map<UpdateCourseCommand>(request);
                command.Code = code;
                await _commandHandler.HandleAsync(command);

                var query = new GetCourseDetailQuery { Code = code };
                var course = await _queryHandler.HandleAsync(query);

                return Ok(course);
            } catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
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
            } catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}