using System.Threading.Tasks;
using IdentityService.Domain.Models;
using IdentityService.Services.Interfaces;

namespace IdentityService.Services.Services
{
    public class TokenService : ITokenService
    {
        public Task<RefreshToken> CreateRefreshTokenForUser(User user, string accessToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRefreshToken(string accessToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
