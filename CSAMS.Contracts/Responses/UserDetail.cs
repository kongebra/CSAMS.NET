using CSAMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Contracts.Responses {
    public class UserDetail {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string DisplayName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Sex? Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
