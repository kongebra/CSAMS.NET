using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Contracts.Responses {
    public class AssignmentDetail {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? PublishAt { get; set; }
        public DateTime? DeadlineAt { get; set; }

        public Guid CourseId { get; set; }
    }
}
