using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSAMS.Commands;
using CSAMS.Commands.Handlers;
using CSAMS.Contracts.Requests;
using CSAMS.Queries;
using CSAMS.Queries.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSAMS.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase {
        private readonly AssignmentCommandHandler _commandHandler;
        private readonly AssignmentQueryHandler _queryHandler;
        private readonly IMapper _mapper;

        public AssignmentController(AssignmentCommandHandler commandHandler, AssignmentQueryHandler queryHandler, IMapper mapper) {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllAssignmentsQuery { };
            var assignments = await _queryHandler.HandleAsync(query);

            return Ok(assignments);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id) {
            var query = new GetAssignmentDetailQuery { Id = id };
            var assignment = await _queryHandler.HandleAsync(query);

            if (assignment == null) {
                return NotFound();
            }
            
            return Ok(assignment);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAssignmentRequest request) {
            try {
                var command = _mapper.Map<CreateAssignmentCommand>(request);
                var id = await _commandHandler.HandleAsync(command);

                var query = new GetAssignmentDetailQuery { Id = id };
                var assignment = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { id = assignment.Id }, assignment);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put([FromBody] UpdateAssignmentRequest request, Guid id) {
            try {
                var command = _mapper.Map<UpdateAssignmentCommand>(request);
                command.Id = id;

                await _commandHandler.HandleAsync(command);

                var query = new GetAssignmentDetailQuery { Id = command.Id };
                var assignment = await _queryHandler.HandleAsync(query);

                return Ok(assignment);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            try {
                var command = new DeleteAssignmentCommand { Id = id };
                await _commandHandler.HandleAsync(command);

                return NoContent();
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}