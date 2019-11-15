using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using IdentityService.Domain;
using IdentityService.Domain.Models;
using IdentityService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace IdentityService.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly ISecurityService _securityService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(
            ILogger<UserService> logger,
            ISecurityService securityService,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _logger = logger;
            _securityService = securityService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<(string, RefreshToken)> AuthorizeUser(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                _logger.LogWarning($"Cannot find user for mail {email}");
                throw new AuthenticationException("Cannot find appropriate user");
            }

            var decryptedPassword = _securityService.DecryptPassword(user.Password, "");
            if (decryptedPassword != password)
            {
                _logger.LogWarning($"Wrong password for {email}");
                throw new AuthenticationException("Wrong password");
            }

            var accessToken = await _securityService.GenerateAccessToken(user.UserId, user.Role.ToString(), TimeSpan.FromHours(2));
            var refreshToken = await _tokenService.CreateRefreshTokenForUser(user, accessToken);

            return (accessToken, refreshToken);
        }

        public async Task RegisterUser(User user)
        {
            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser != null)
            {
                _logger.LogError($"Cannot register user. Duplicated email {user.Email}");
                throw new ArgumentException("Cannot register user");
            }

            user.Password = _securityService.EncryptPassword(user.Password, "");

            await _userRepository.CreateUser(user);
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser == null)
            {
                _logger.LogError($"Cannot update user. Cannot find for email {user.Email}");
                throw new ArgumentException("Cannot update user");
            }

            await _userRepository.UpdateUser(user);
        }

        public async Task RemoveUser(User user)
        {
            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser == null)
            {
                _logger.LogError($"Cannot remove user for email {user.Email}");
                throw new ArgumentException("Cannot find user");
            }

            await _userRepository.RemoveUser(user.UserId);
        }
    }
}
