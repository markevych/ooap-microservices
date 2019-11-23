using Common.Domain.Models;

namespace IdentityService.Services.Interfaces
{
    public interface IUserService
    {
        (string, User) AuthorizeUser(string login, string password);
    }
}
