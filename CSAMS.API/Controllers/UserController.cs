using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CSAMS.Commands;
using CSAMS.Commands.Handlers;
using CSAMS.Contracts.Requests;
using CSAMS.Queries;
using CSAMS.Queries.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSAMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase {
        private readonly UserCommandHandler _commandHandler;
        private readonly UserQueryHandler _queryHandler;
        private readonly IMapper _mapper;

        public UserController(
            UserCommandHandler commandHandler, 
            UserQueryHandler queryHandler, 
            IMapper mapper) {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllUsersQuery { };
            var users = await _queryHandler.HandleAsync(query);

            return Ok(users);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult> Get(string username) {
            var query = new GetUserByUsernameQuery { Username = username };
            var user = await _queryHandler.HandleAsync(query);

            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegisterUserRequest request) {
            try {
                var command = _mapper.Map<RegisterUserCommand>(request);
                await _commandHandler.HandleAsync(command);

                var query = new GetUserByUsernameQuery { Username = command.Username };
                var user = await _queryHandler.HandleAsync(query);

                return CreatedAtAction(nameof(Get), new { username = query.Username }, user);
            } catch (Exception ex) {
                // Check if exception is cause of duplicated key
                if (ex.InnerException.Message.Contains("duplicate key")) {
                    return BadRequest(new {
                        title = $"Username or email is already in use.",
                        status = HttpStatusCode.BadRequest,
                    });
                }

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{username}")]
        public async Task<ActionResult> Put([FromBody] UpdateUserDetailRequest request, string username) {
            try {
                var command = _mapper.Map<UpdateUserDetailCommand>(request);
                command.Username = username;
                await _commandHandler.HandleAsync(command);

                return Ok();
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{username}")]
        public async Task<ActionResult> Delete(string username) {
            try {
                var command = new DeleteUserCommand { Username = username };
                await _commandHandler.HandleAsync(command);

                return NoContent();
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult> Search() {
            try {
                var firstName = Request.Query["firstName"];
                var lastName = Request.Query["lastName"];
                var email = Request.Query["email"];
                var username = Request.Query["username"];
                DateTime dateOfBirth;
                if (!DateTime.TryParse(Request.Query["dateOfBirth"], out dateOfBirth)) {
                    return BadRequest($"{Request.Query["dateOfBirth"]} is an invalid value for 'dateOfBirth'");
                }

                var query = new UserSearchQuery {
                    Query = new {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Username = username,
                        DateOfBirth = dateOfBirth,
                    },
                };

                var users = await _queryHandler.HandleAsync(query);

                return Ok(users);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("verify/{email}")]
        public async Task<ActionResult> VerifyEmail(string email) {
            try {
                var command = new VerifyEmailCommand { Email = email };
                await _commandHandler.HandleAsync(command);

                return Ok();
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}