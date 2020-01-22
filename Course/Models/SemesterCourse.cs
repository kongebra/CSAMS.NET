using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Models {
    public class SemesterCourse {
        public Guid Id { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
