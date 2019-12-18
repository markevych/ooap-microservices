using System.Collections.Generic;
using System.Linq;
using Common.Domain.Interfaces.Persistence;
using Common.Domain.Interfaces.Security;
using Common.Domain.Models;
using DiaryService.Services;
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
        private readonly ISecurityService _securityService;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IDiaryService _diaryService;

        public DiaryController(
            ILogger<DiaryController> logger,
            ISecurityService securityService,
            IDiaryService diaryService,
            ITeacherRepository teacherRepository)
        {
            _logger = logger;
            _securityService = securityService;
            _diaryService = diaryService;
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public ActionResult<List<StudentResultResponse>> Get()
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);

            return 
                _diaryService.GetStudentResults(userId)
                    .Select(result => new StudentResultResponse(result))
                    .ToList();
        }

        [HttpPut("{studentResultId}")]
        public ActionResult<StudentResultResponse> UpdateResult(int studentResultId, [FromBody] StudentResultRequest request)
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);
            
            var teacher = _teacherRepository.FindById(request.TeacherId);
            if (teacher == null || teacher.UserId != userId)
                return BadRequest("Cannot update");

            return
                new StudentResultResponse(_diaryService.UpdateUserResult(studentResultId, request.Point, request.WasPresent));
        }

        [HttpPost("subjects/{subjectId}/students/{studentId}")]
        public ActionResult<StudentResultResponse> CreateResult(int subjectId, int studentId, [FromBody] StudentResultRequest request)
        {
            var userId = _securityService.FetchUserId(HttpContext.User.Claims);

            var teacher = _teacherRepository.FindById(request.TeacherId);
            if (teacher == null || teacher.UserId != userId)
                return BadRequest("Cannot update");

            return 
                new StudentResultResponse(_diaryService.CreateUserResult(studentId, subjectId, teacher, request.Point, request.WasPresent));
        }

        public class StudentResultRequest
        {
            public int TeacherId { get; set; }
            public int? Point { get; set; }
            public bool WasPresent { get; set; }
        }

        public class StudentResultResponse
        {
            public int Id { get; set; }
            public int? Point { get; set; }
            public bool WasPresent { get; set; }
            public int TeacherId { get; set; }
            public int StudentId { get; set; }
            public int SubjectId { get; set; }

            public StudentResultResponse()
            {
            }

            public StudentResultResponse(StudentResult result)
            {
                Id = result.Id;
                TeacherId = result.TeacherSubject.TeacherId;
                SubjectId = result.TeacherSubject.SubjectId;
                Point = result.Point;
                WasPresent = result.WasPresent;
                StudentId = result.StudentId;
            }
        }
    }
}
