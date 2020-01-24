using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CSAMS.Commands.Handlers;
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
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id) {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object request) {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put([FromBody] object request, Guid id) {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            return NoContent();
        }
    }
}