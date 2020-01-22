using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models {
    public class Student : Person {
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
