using System.Threading.Tasks;
using IdentityService.Domain.Models;

namespace IdentityService.Services.Interfaces
{
    public interface IUserService
    {
        Task<(string, RefreshToken)> AuthorizeUser(string email, string password);
        Task RegisterUser(User user);
        Task UpdateUser(User user);
        Task RemoveUser(User user);
    }
}
