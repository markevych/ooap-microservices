using System;
using System.Linq;
using Common.Domain.Enums;
using Common.Domain.Interfaces.Persistence;
using Common.Domain.Interfaces.Security;
using Common.Domain.Models;
using IdentityService.Services.Interfaces;
using IdentityService.Services.Models;

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

            var accessToken = _securityService.GenerateAccessToken(user, UserRole.Student.ToString(), TimeSpan.FromHours(1));

            return (accessToken, user);
        }

        public User CreateUser(User user, UserRole userRole)
        {
            ValidateUser(user);

            var existingUser = _userRepository
                .Get(u => u.Email == user.Email)
                .FirstOrDefault();
            if (existingUser != null)
            {
                throw new ArgumentOutOfRangeException("Already used email");
            }

            return _userRepository.Create(user);
        }

        public User GetUser(int id)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException($"Cannot find user with id {id}");
            }

            return user;
        }

        public void RemoveUser(int id)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException("Cacnot find appropriate user");
            }

            _userRepository.Remove(user);
        }

        public User UpdateUser(UpdateUserModel updateModel)
        {
            var updaterUser = _userRepository.FindById(updateModel.UpdaterUserId);
            if (updaterUser == null)
            {
                throw new ArgumentOutOfRangeException("Cannot find updater user");
            }

            if (updaterUser.Id != updateModel.UserId && updaterUser.UserRole != UserRole.SuperAdmin)
            {
                throw new FieldAccessException();
            }

            var user = _userRepository.FindById(updateModel.UserId);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException("Cannot find user to update");
            }

            user.Email = updateModel.NewEmail;
            user.FullName = updateModel.NewName;
            user.UserRole = updateModel.NewRole;

            ValidateUser(user);

            var existingUser = _userRepository
                .Get(u => u.Email == user.Email)
                .FirstOrDefault();
            if (existingUser != null && existingUser.Email != updateModel.NewEmail)
            {
                throw new ArgumentOutOfRangeException("Already used email");
            }

            _userRepository.Update(user);

            return user;
        }

        private static void ValidateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email)) // add logic for checking email pattern
            {
                throw new ArgumentException("Bad formatted email");
            }

            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                throw new ArgumentException("Bad formatted user name or surname");
            }

            if (user.UserRole == UserRole.SuperAdmin)
            {
                throw new ArgumentOutOfRangeException("Cannot assign super admin role for this user");
            }
        }
    }
}
