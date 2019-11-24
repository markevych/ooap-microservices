using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Common.Domain.Interfaces.Security;
using Common.Domain.Models;

namespace Common.Auth
{
    public class SecurityService : ISecurityService
    {
        public const string UserIdClaimName = "UserId";

        public string GenerateAccessToken(User user, string userRole, TimeSpan expirationDuration)
        {
            var claims = new[]
            {
                new Claim(UserIdClaimName, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthenticationExtensions.AuthSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(expirationDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int FetchUserId(IEnumerable<Claim> claims)
        {
            if (!int.TryParse(claims.FirstOrDefault(claim => claim.Type == UserIdClaimName)?.Value, out var userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }

        public string EncryptPassword(string password, string salt)
        {
            throw new NotImplementedException();
        }

        public string DecryptPassword(string passwordHash, string salt)
        {
            // TO DO implement password hashing
            return passwordHash;
        }
    }
}