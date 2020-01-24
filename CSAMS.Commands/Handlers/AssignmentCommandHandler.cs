using CSAMS.Contracts.Interfaces;
using CSAMS.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Commands.Handlers {
    public class AssignmentCommandHandler {
        private readonly IAssignmentRepository _repository;
        private readonly CommandStoreService _commandStoreService;

        public AssignmentCommandHandler(IAssignmentRepository repository, CommandStoreService commandStoreService) {
            _repository = repository;
            _commandStoreService = commandStoreService;
        }
    }
}
