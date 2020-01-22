using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models {
    public class OfficeAssignment {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
