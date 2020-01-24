using AutoMapper;
using CSAMS.Contracts.Interfaces;
using CSAMS.Contracts.Responses;
using CSAMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAMS.Queries.Handlers {
    public class AssignmentQueryHandler {
        private readonly IAssignmentRepository _repository;
        private readonly IMapper _mapper;

        public AssignmentQueryHandler(IAssignmentRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssignmentInfo>> HandleAsync(GetAllAssignmentsQuery _) {
            var assignments = await _repository.GetAll();

            return assignments.Select(assignment => {
                return _mapper.Map<AssignmentInfo>(assignment);
            });
        }

        public async Task<AssignmentDetail> HandleAsync(GetAssignmentDetailQuery query) {
            var assignment = await _repository.GetById(query.Id);

            if (assignment == null) {
                throw new EntityNotFoundException($"Assignment with ID '{query.Id}' Not Found.");
            }

            return _mapper.Map<AssignmentDetail>(assignment);
        }
    }
}
