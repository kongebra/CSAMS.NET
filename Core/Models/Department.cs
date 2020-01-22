using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models {
    public class Department : IEntity {
        #region IEntity
        [Column("DepartmentId")]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion

        public string Name { get; set; }

        public Guid? TeacherId { get; set; }
        public Teacher Administrator { get; set; }

        public ICollection<Course> Courses { get; set; } 
    }
}
