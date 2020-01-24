using CSAMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Contracts.Requests {
    public class UpdateUserDetailRequest {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public string DisplayName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
