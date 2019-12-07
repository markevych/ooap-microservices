using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityService.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("storage")]
    public class StorageController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StorageController(IFileService fileService, IHostingEnvironment hostingEnvironment)
        {
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{path}")]
        public HttpResponseMessage GetFile(string path)
        {
            var fileContent = _fileService.GetFile($"{_hostingEnvironment.ContentRootPath}/{path}");
            if (fileContent == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileContent)
            };

            var fileName = System.IO.Path.GetFileName(path);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue(fileName);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            return result;
        }
    }
}
