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
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase {
        private readonly TeacherQueryHandler _queryHandler;
        private readonly TeacherCommandHandler _commandHandler;
        private readonly IMapper _mapper;

        public TeacherController(
            TeacherQueryHandler queryHandler,
            TeacherCommandHandler commandHandler,
            IMapper mapper) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllTeachersQuery { };
            var teachers = await _queryHandler.HandleAsync(query);

            return Ok(teachers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id) {
            var query = new GetTeacherDetailQuery { Id = id };
            var teacher = await _queryHandler.HandleAsync(query);

            if (teacher == null) {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateTeacherRequest request) {
            try {
                var command = _mapper.Map<CreateTeacherCommand>(request);
                await _commandHandler.HandleAsync(command);

                var query = new GetTeacherByEmailQuery { Email = request.Email };
                var teacher = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { id = teacher.Id }, teacher);
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}