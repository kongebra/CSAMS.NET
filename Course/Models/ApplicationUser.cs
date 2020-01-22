using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSAMS.Course.Models {
    public class ApplicationUser : IdentityUser {
        public string CustomTag { get; set; }
        public string CustomTagBis { get; set; }
    }
}
