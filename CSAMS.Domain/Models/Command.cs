using System;
using System.Collections.Generic;
using System.Text;

namespace CSAMS.Domain.Models {
    public class Command {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
