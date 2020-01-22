using System;

namespace CSAMS.Course.Exceptions {
    public class CourseAlreadyExistsException : Exception {
        public CourseAlreadyExistsException(string message) : base(message) { }

        public CourseAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
