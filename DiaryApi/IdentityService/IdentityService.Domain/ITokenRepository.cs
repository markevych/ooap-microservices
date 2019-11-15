using System.Threading.Tasks;
using IdentityService.Domain.Models;

namespace IdentityService.Domain
{
    public interface ITokenRepository
    {
        Task<RefreshToken> GetToken(int id);
        Task RemoveToken(int id);
        Task CreateToken(RefreshToken token);
    }
}
