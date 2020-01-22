using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converto;
using CSAMS.Course.Commands;
using CSAMS.Course.Commands.Handlers;
using CSAMS.Course.Exceptions;
using CSAMS.Course.Queries.Handlers;
using CSAMS.Course.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CSAMS.Course.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller {
        private readonly StudentQueryHandler _queryHandler;
        private readonly StudentCommandHandler _commandHandler;

        public StudentController(StudentQueryHandler queryHandler, StudentCommandHandler commandHandler) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Post([FromBody] RegisterStudentRequest request) {
            try {
                var command = request.ConvertTo<RegisterStudentUserCommand>();
                await _commandHandler.HandleAsync(command);


                return Ok();
            } catch (UserNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }

}