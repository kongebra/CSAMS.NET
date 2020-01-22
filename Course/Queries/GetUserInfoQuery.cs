using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Queries {
    public class GetUserInfoQuery {
        public string Username { get; set; }

        public GetUserInfoQuery(string username) {
            Username = username;
        }
    }
}
