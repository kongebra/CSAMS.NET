using CSAMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSAMS.Domain.Models {
    public class User {
        #region Base Entity
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion

        #region Basic User Information
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public UserRole Role { get; set; }
        #endregion

        #region Additional User Information
        public string DisplayName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Sex? Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool EmailVerified { get; set; }
        #endregion
    }
}
