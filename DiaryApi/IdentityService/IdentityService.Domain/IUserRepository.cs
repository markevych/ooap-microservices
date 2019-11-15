using System.Threading.Tasks;
using IdentityService.Domain.Models;

namespace IdentityService.Domain
{
    public interface IUserRepository
    {
        Task<User[]> GetAll();
        Task<User> GetByEmail(string email);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task RemoveUser(int userId);
    }
}
