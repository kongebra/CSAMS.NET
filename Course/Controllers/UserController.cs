using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converto;
using CSAMS.Course.Commands;
using CSAMS.Course.Commands.Handlers;
using CSAMS.Course.Models;
using CSAMS.Course.Queries;
using CSAMS.Course.Queries.Handlers;
using CSAMS.Course.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSAMS.Course.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller {
        private readonly UserQueryHandler _queryHandler;
        private readonly UserCommandHandler _commandHandler;

        public UserController(
            UserQueryHandler queryHandler,
            UserCommandHandler commandHandler
            ) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllUsersQuery();
            var users = await _queryHandler.HandleAsync(query);

            return Ok(users);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult> Get(string username) {
            try {
                var query = new GetUserInfoQuery(username);
                var user = await _queryHandler.HandleAsync(query);

                if (user == null) {
                    return NotFound();
                }

                return Ok(user);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateUserRequest request) {
            try {
                var command = request.ConvertTo<CreateUserCommand>();
                await _commandHandler.HandleAsync(command);


                var query = new GetUserByPrivateEmailQuery(request.Email);
                var user = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { username = user.Username }, user);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}