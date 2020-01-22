using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models {
    public enum Grade {
        A, B, C, D, E, F
    }

    public class Enrollment : IEntity {
        #region IEntity
        [Column("EnrollmentId")]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }
    }
}
