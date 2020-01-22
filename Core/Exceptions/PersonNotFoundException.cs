using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Exceptions {
    public class PersonNotFoundException : Exception {
        public PersonNotFoundException() { }
        public PersonNotFoundException(string message) : base(message) { }
        public PersonNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
