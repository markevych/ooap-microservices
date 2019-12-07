using System.Threading.Tasks;
using Common.Domain.Enums;
using Common.Domain.Models;
using IdentityService.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IdentityService.Services.Interfaces
{
    public interface IUserService
    {
        (string, User) AuthorizeUser(string login, string password);
        User CreateUser(User user, UserRole userRole);
        User GetUser(int id);
        void RemoveUser(int id);
        Task<User> UpdateUser(UpdateUserModel updateModel, IFormFile newUserImage, IUrlHelper urlHelper);
    }
}
