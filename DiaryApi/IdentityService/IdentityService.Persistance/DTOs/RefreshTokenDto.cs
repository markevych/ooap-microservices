using System;
using IdentityService.Domain.Models;

namespace IdentityService.Persistence.DTOs
{
    public class RefreshTokenDto
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }
        public DateTime Created { get; set; }
    }

    public static class RefreshTokenExtensions
    {
        public static RefreshToken ToRefreshToken(this RefreshTokenDto dto, User user)
        {
            return new RefreshToken
            {
                Id = dto.Id,
                Value = dto.Value,
                Created = dto.Created,
                User = user
            };
        }

        public static RefreshTokenDto ToRefreshTokenDto(this RefreshToken model)
        {
            return new RefreshTokenDto
            {
                Id = model.Id,
                Value = model.Value,
                Created = model.Created,
                UserId = model.User.UserId
            };
        }
    }
}
