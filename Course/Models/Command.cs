using System;

namespace CSAMS.Course.Models {
    public class Command {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
    }
}
