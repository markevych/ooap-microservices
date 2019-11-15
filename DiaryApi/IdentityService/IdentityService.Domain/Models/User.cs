using System;

namespace IdentityService.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
