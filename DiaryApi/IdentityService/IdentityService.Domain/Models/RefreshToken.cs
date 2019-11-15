using System;

namespace IdentityService.Domain.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Value { get; set; }
        public DateTime Created { get; set; }
    }
}
