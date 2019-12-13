using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("diaries")]
    public class DiaryController : ControllerBase
    {
        private readonly ILogger<DiaryController> _logger;

        public DiaryController(ILogger<DiaryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "";
        }
    }
}
