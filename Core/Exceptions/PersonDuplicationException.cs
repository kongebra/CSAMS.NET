using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Exceptions {
    public class PersonDuplicationException : Exception {

        public PersonDuplicationException() {
        }
        public PersonDuplicationException(string message) : base(message) {
        }

        public PersonDuplicationException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
