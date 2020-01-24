using System;
using System.ComponentModel.DataAnnotations;

namespace CSAMS.Domain.Models {

    public class Course {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Descripion { get; set; }
    }
}