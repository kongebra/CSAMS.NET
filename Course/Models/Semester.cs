using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Models {
    public class Semester {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public SemesterPart Part { get; set; }
    }

    public enum SemesterPart {
        Fall = 0,
        Spring,
    }
}
