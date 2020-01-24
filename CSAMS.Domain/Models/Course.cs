using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSAMS.Domain.Models {

    public class Course {
        #region Base Entity
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion

        #region Basic Course Information
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        #endregion

        public ICollection<Assignment> Assignments { get; set; }
    }
}