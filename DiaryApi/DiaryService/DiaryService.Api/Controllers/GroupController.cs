using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("diaries")]
    public class GroupController : ControllerBase
    {
    }
}
