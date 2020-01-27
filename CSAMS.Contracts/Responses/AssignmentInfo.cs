using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Contracts.Responses {
    public class AssignmentInfo {
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid CourseId { get; set; }
    }
}
