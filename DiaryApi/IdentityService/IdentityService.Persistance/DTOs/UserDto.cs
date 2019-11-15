using System;
using IdentityService.Domain.Models;

namespace IdentityService.Persistence.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public static class UserExtensions
    {
        public static User ToUser(this UserDto dto)
        {
            return new User
            {
                UserId = dto.UserId,
                Name = dto.Name,
                Surname = dto.Surname,
                Password = dto.Password,
                Email = dto.Email,
                Role = (UserRole)dto.Role,
                Created = dto.Created,
                Updated = dto.Updated
            };
        }

        public static UserDto ToUser(this User model)
        {
            return new UserDto
            {
                UserId = model.UserId,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password,
                Email = model.Email,
                Role = (byte)model.Role,
                Created = model.Created,
                Updated = model.Updated
            };
        }
    }
}
