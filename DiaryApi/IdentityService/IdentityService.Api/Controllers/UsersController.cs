using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.Domain.Enums;
using Common.Domain.Interfaces.Security;
using Common.Domain.Models;
using IdentityService.Services.Interfaces;
using IdentityService.Services.Models;

namespace IdentityService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly ISecurityService _securityService;

        public UsersController(
            ILogger<UsersController> logger,
            IUserService userService,
            ISecurityService securityService)
        {
            _logger = logger;
            _userService = userService;
            _securityService = securityService;
        }

        [HttpPost]
        public ActionResult<int> CreateUser([FromBody] RegisterUserRequest request)
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);
            if (!IsSuperAdmin(userId))
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }

            var user = _userService.CreateUser(
                new User
                {
                    FullName = $"{request.UserName} {request.UserSurname}",
                    Email = request.Email,
                    PasswordHash = request.Password,
                    UserRole = request.UserRole
                },
                request.UserRole);

            _logger.LogInformation($"New user with id {user.Id} was created");

            return user.Id;
        }

        public class RegisterUserRequest
        {
            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public UserRole UserRole { get; set; }
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveUser(int id)
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);
            if (!IsSuperAdmin(userId))
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }

            _userService.RemoveUser(id);

            _logger.LogInformation($"User with id {id} was removed");

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);
            if (userId.ToString() != request.UserId && !IsSuperAdmin(userId))
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }

            if (!Enum.TryParse(request.UserRole, out UserRole newRole))
            {
                return BadRequest("Cannot parse new user role");
            }

            var newUser = await _userService.UpdateUser(
                new UpdateUserModel
                {
                    UpdaterUserId = int.Parse(request.UserId),
                    UserId = int.Parse(request.UserId),
                    NewEmail = request.Email,
                    NewRole = newRole,
                    NewName = $"{request.UserName} {request.UserSurname}",
                    UserImage = request.UserImage
                });

            _logger.LogInformation($"User with id {request.UserId} was updated");

            return
                new UserResponse(newUser.Id, newUser.Email, newUser.FullName, newUser.UserRole, GetUserImage(newUser));
        }

        public class UpdateUserRequest
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public string Email { get; set; }
            public string UserRole { get; set; }
            public IFormFile UserImage { get; set; }
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponse> GetUser(int id)
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);
            if (userId != id && !IsSuperAdmin(userId))
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }

            var user = _userService.GetUser(id);

            return
                new UserResponse(user.Id, user.Email, user.FullName, user.UserRole, GetUserImage(user));
        }

        [HttpGet]
        public ActionResult<UserResponse> GetOwnUser()
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);
            var user = _userService.GetUser(userId);

            return
                new UserResponse(user.Id, user.Email, user.FullName, user.UserRole, GetUserImage(user));
        }

        public class UserResponse
        {
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public UserRole Role { get; set; }
            public FileContentResult UserImage { get; set; }

            public UserResponse(int userId, string fullName, string email, UserRole role, FileContentResult image)
            {
                UserId = userId;
                FullName = fullName;
                Email = email;
                Role = role;
                UserImage = image;
            }
        }

        private FileContentResult GetUserImage(User user)
        {
            return user.Image != null
                ? File(user.Image, "application/octet-stream")
                : null;
        }
        private bool IsSuperAdmin(int id)
        {
            // TO DO move super admin credentials in appSettings file
            var invokeUser = _userService.GetUser(id);

            return invokeUser?.UserRole == UserRole.SuperAdmin;
        }
    }
}
