using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models {
    public class CourseAssignment {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
