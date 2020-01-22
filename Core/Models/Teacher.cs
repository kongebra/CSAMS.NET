using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models {
    public class Teacher : Person {
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
