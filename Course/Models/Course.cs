using System;

namespace CSAMS.Course.Models {
    public class Course {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
