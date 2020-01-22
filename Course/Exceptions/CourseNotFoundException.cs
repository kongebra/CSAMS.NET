using System;

namespace CSAMS.Course.Exceptions {
    public class CourseNotFoundException : Exception {
        public CourseNotFoundException(string message) : base(message) { }

        public CourseNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
