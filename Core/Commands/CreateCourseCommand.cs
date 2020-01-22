using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Commands {
    public class CreateCourseCommand {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
    }
}
