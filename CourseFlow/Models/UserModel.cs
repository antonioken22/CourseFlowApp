﻿using System.Security;

namespace CourseFlow.Models
{
    public class UserModel : AuditableEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; } // For hashing
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Admin, Student, Faculty
        public string ProfilePicture { get; set; }
    }
}
