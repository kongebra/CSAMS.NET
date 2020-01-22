using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Queries {
    public class GetUserByPrivateEmailQuery {
        public string PrivateEmail { get; set; }

        public GetUserByPrivateEmailQuery(string email) {
            PrivateEmail = email;
        }
    }
}
