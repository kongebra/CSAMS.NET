using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Responses {
    public class UserInfo {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        // Delete these after testing!!!
        public Guid Id { get; set; }
        public string PrivateEmail { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
