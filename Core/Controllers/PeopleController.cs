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
    public class PeopleController : ControllerBase {
        private readonly PersonQueryHandler _queryHandler;
        private readonly PersonCommandHandler _commandHandler;
        private readonly IMapper _mapper;

        public PeopleController(
            PersonQueryHandler queryHandler,
            PersonCommandHandler commandHandler,
            IMapper mapper) {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get() {
            var query = new GetAllPeopleQuery { };
            var people = await _queryHandler.HandleAsync(query);

            return Ok(people);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id) {
            var query = new GetPersonDetailQuery { Id = id };
            var person = await _queryHandler.HandleAsync(query);

            if (person == null) {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult> Search() {
            var firstName = Request.Query["firstName"];
            var lastName = Request.Query["lastName"];
            var email = Request.Query["email"];

            var query = new SearchForPersonQuery {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };
            var people = await _queryHandler.HandleAsync(query);

            if (!people.Any()) {
                return NotFound();
            }

            return Ok(people);
        }
    }
}