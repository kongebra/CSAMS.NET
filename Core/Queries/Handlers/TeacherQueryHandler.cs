using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Queries.Handlers {
    public class TeacherQueryHandler {
        private readonly ITeacherRepository _repository;

        public TeacherQueryHandler(ITeacherRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<TeacherInfo>> HandleAsync(GetAllTeachersQuery _) {
            var teachers = await _repository.GetAll();

            return teachers.Select(teacher => {
                return new TeacherInfo {
                    Name = teacher.FullName,
                    Email = teacher.Email,
                };
            });
        }

        public async Task<TeacherDetail> HandleAsync(GetTeacherDetailQuery query) {
            var teacher = await _repository
                .FirstOrDefault(t => t.Id.Equals(query.Id));

            if (teacher == null) {
                throw new PersonNotFoundException($"Person with id '{query.Id}' not found.");
            }

            return new TeacherDetail {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                DateOfBirth = teacher.DateOfBirth,
            };
        }

        public async Task<TeacherDetail> HandleAsync(GetTeacherByEmailQuery query) {
            var teacher = await _repository
                .FirstOrDefault(t => t.Email.ToLower() == query.Email.ToLower());

            if (teacher == null) {
                throw new PersonNotFoundException($"Teacher with email '{query.Email}' not found.");
            }

            return new TeacherDetail {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                DateOfBirth = teacher.DateOfBirth,
            };
        }
    }
}
