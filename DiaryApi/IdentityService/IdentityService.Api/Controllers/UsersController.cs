using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Common.Domain.Models;
using IdentityService.Services.Interfaces;

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

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            await _userService.RegisterUser(
                new User
                {
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
