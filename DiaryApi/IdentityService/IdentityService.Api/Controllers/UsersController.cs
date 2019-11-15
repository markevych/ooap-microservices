using System;
using System.Threading.Tasks;
using IdentityService.Domain.Models;
using IdentityService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<UsersController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateRequest request)
        {
            var (accessToken, refreshToken) = await _userService.AuthorizeUser(request.Login, request.Password);

            return new AuthenticateResponse
            {
                AccessToken = accessToken,
                UserId = refreshToken.User.UserId,
                RefreshToken = refreshToken.Value
            };
        }

        public class AuthenticateRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public class AuthenticateResponse
        {
            public int UserId { get; set; }
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            await _userService.RegisterUser(
                new User
                {
                    Name = request.UserName,
                    Surname = request.UserSurname,
                    Password = request.Password,
                    Email = request.Email,
                    Role = request.UserRole
                });

            return Ok();
        }

        public class CreateUserRequest
        {
            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public UserRole UserRole { get; set; }
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public class UpdateUserRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string UserRole { get; set; }
        }

        [HttpDelete]
        public ActionResult RemoveUser()
        {
            throw new NotImplementedException();
        }
    }
}
