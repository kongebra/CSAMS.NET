using System;

namespace CSAMS.Domain.Models {

    public class Course {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Descripion { get; set; }
    }
}