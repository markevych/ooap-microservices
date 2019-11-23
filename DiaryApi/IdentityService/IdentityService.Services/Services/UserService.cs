using System;
using System.Linq;
using Common.Auth;
using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using IdentityService.Services.Interfaces;

namespace IdentityService.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ISecurityService _securityService;
        private readonly IUserRepository _userRepository;

        public UserService(
            ISecurityService securityService,
            IUserRepository userRepository)
        {
            _securityService = securityService;
            _userRepository = userRepository;
        }

        public (string, User) AuthorizeUser(string login, string password)
        {
            var user = _userRepository.Get(u => u.Email == login).SingleOrDefault();
            if (user == null)
            {
                throw new ArgumentOutOfRangeException("Wrong password or email");
            }

            if (user.PasswordHash != _securityService.DecryptPassword(password, ""))
            {
                throw new ArgumentOutOfRangeException("Wrong password or email");
            }

            var accessToken = _securityService.GenerateAccessToken(user.Id, user.Email, UserRole.Student.ToString(), TimeSpan.FromHours(1));

            return (accessToken, user);
        }
    }
}
