using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Contracts.Responses {
    public class CourseDetail {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
