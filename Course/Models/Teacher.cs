using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Models {
    public class Teacher : ApplicationUser {
        public string TeachIdentificationNumber { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
