using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Queries.Handlers {
    public class PersonQueryHandler {
        private readonly IPersonRepository _repository;

        public PersonQueryHandler(IPersonRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonInfo>> HandleAsync(GetAllPeopleQuery _) {
            var people = await _repository.GetAll();

            return people.Select(teacher => {
                return new PersonInfo {
                    Name = teacher.FullName,
                    Email = teacher.Email,
                };
            });
        }

        public async Task<PersonDetail> HandleAsync(GetPersonDetailQuery query) {
            var person = await _repository
                .FirstOrDefault(t => t.Id.Equals(query.Id));

            if (person == null) {
                throw new PersonNotFoundException($"Person with id '{query.Id}' not found.");
            }

            return new PersonDetail {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone,
                DateOfBirth = person.DateOfBirth,
            };
        }

        public async Task<PersonDetail> HandleAsync(GetPersonByEmailQuery query) {
            var person = await _repository
                .FirstOrDefault(t => t.Email.ToLower() == query.Email.ToLower());

            if (person == null) {
                throw new PersonNotFoundException($"Person with email '{query.Email}' not found.");
            }

            return new PersonDetail {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone,
                DateOfBirth = person.DateOfBirth,
            };
        }

        public async Task<IEnumerable<Person>> HandleAsync(SearchForPersonQuery query) {
            return await _repository.Query(query);
        }
    }
}
