using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Common.Auth
{
    public class SecurityService : ISecurityService
    {
        private readonly string _authSecret = "safasfASFASfa89y9132ytrh9euofGS(FGg03gha09sfgaw98fga9wfguoasjfhasouf98qwgf9qw8gfljBOUG";
        private readonly TimeSpan _expirationDuration = TimeSpan.FromHours(1);

        public const string UserIdClaimName = "UserId";

        public string GenerateAccessToken(int userId, string email, string userRole)
        {
            var claims = new []
            {
                new Claim(UserIdClaimName, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, userRole)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_authSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(_expirationDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<bool> VerifyToken(string token, int userId)
        {
            throw new NotImplementedException();
        }

        public string EncryptPassword(string password, string salt)
        {
            throw new NotImplementedException();
        }

        public string DecryptPassword(string passwordHash, string salt)
        {
            throw new NotImplementedException();
        }
    }
}
