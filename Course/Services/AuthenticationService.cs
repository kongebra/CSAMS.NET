using System;

namespace CSAMS.Course.Services {
    public class AuthenticationService {
        public readonly Guid _userId;

        public AuthenticationService() {
            _userId = Guid.NewGuid();
        }

        public Guid GetUserId() => _userId;
    }
}
