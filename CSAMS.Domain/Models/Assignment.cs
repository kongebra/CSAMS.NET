using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Domain.Models {
    public class Assignment {
        #region Base Entity
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion

        #region Basic Assignment Information
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime PublishAt { get; set; }
        public DateTime DeadlineAt { get; set; }
        #endregion

        #region Course Relation
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}
