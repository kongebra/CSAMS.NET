using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Exceptions {
    public class EmailAlreadyInUseException : Exception {
        public EmailAlreadyInUseException(string message) : base(message) {
        }

        public EmailAlreadyInUseException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
