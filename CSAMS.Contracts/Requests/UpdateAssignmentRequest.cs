using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Contracts.Requests {
    public class UpdateAssignmentRequest {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? PublishAt { get; set; }
        public DateTime? DeadlineAt { get; set; }
    }
}
