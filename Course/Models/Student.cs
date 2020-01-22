using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Models {
    public class Student : ApplicationUser {
        public ICollection<StudentGroup> Groups { get; set; }
    }
}
