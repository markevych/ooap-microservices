using System.Threading.Tasks;
using IdentityService.Domain.Models;

namespace IdentityService.Services.Interfaces
{
    public interface ITokenService
    {
        Task<RefreshToken> CreateRefreshTokenForUser(User user, string accessToken);
        Task RemoveRefreshToken(string accessToken);
    }
}
