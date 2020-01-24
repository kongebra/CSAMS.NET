using AutoMapper;
using CSAMS.Contracts.Interfaces;
using CSAMS.Contracts.Responses;
using CSAMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.Queries.Handlers {
    public class UserQueryHandler {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserQueryHandler(IUserRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDetail> HandleAsync(GetUserByEmailQuery query) {
            var user = await _repository.GetByEmail(query.Email);

            if (user == null) {
                return null;
            }

            return _mapper.Map<UserDetail>(user);
        }

        public async Task<IEnumerable<UserInfo>> HandleAsync(GetAllUsersQuery _) {
            var users = await _repository.GetAll();

            return users.Select(user => {
                return _mapper.Map<UserInfo>(user);
            });
        }

        public async Task<UserDetail> HandleAsync(GetUserByIdQuery query) {
            var user = await _repository.GetById(query.Id);

            if (user == null) {
                return null;
            }

            return _mapper.Map<UserDetail>(user);
        }

        public async Task<UserDetail> HandleAsync(GetUserByUsernameQuery query) {
            var user = await _repository.GetByUsername(query.Username);

            if (user == null) {
                return null;
            }

            return _mapper.Map<UserDetail>(user);
        }

        public async Task<IEnumerable<UserDetail>> HandleAsync(UserSearchQuery query) {
            var users = await _repository.Search(query.Query) as List<User>;

            return users.Select(user => {
                return _mapper.Map<UserDetail>(user);
            });
        }
    }
}
