using CSAMS.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Queries.Handlers {
    public class AssignmentQueryHandler {
        private readonly IAssignmentRepository _repository;

        public AssignmentQueryHandler(IAssignmentRepository repository) {
            _repository = repository;
        }


    }
}
