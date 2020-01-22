using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Requests {
    public class CreateCourseRequest {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Credits { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
