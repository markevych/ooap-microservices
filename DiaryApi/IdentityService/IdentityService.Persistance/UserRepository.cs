using System.Threading.Tasks;
using IdentityService.Domain;
using IdentityService.Domain.Models;

namespace IdentityService.Persistence
{
    public class UserRepository : IUserRepository
    {
        public Task<User[]> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUser(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
