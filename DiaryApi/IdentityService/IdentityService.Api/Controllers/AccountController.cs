﻿using IdentityService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public AccountController(
            ILogger<UsersController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        public ActionResult<AuthenticateResponse> Authenticate([FromBody] AuthenticateRequest request)
        {
            var (accessToken, user) = _userService.AuthorizeUser(request.Login, request.Password);

            return new AuthenticateResponse
            {
                AccessToken = accessToken,
                UserId = user.Id,
                // TO DO RefreshToken = refreshToken.Value
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
    }
}
