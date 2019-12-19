using System.Collections.Generic;
using System.Linq;
using Common.Domain.Interfaces.Persistence;
using Common.Domain.Interfaces.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("diaries")]
    public class GroupController : ControllerBase
    {
        //private readonly ILogger<GroupController> _logger;
        //private readonly ISecurityService _securityService;
        //private readonly IGroupRepository _groupRepository;
        //private readonly IStudentRepository _studentRepository;
        //private readonly ITeacherRepository _teacherRepository;

        //public GroupController(
        //    ILogger<GroupController> logger,
        //    ISecurityService securityService,
        //    IGroupRepository groupRepository,
        //    IStudentRepository studentRepository,
        //    ITeacherRepository teacherRepository)
        //{
        //    _logger = logger;
        //    _securityService = securityService;
        //    _groupRepository = groupRepository;
        //    _studentRepository = studentRepository;
        //    _teacherRepository = teacherRepository;
        //}

        //[HttpGet("groups/{groupId}")]
        //public ActionResult<List<DiaryController.StudentResultResponse>> Get(int groupId)
        //{
        //    var group = _groupRepository.GetWithInclude(g => g.Id == groupId, g => g.TeacherSubjects, g => g.Students).FirstOrDefault();

        //    if (group == null)
        //        return new NotFoundResult();

        //    return _su
        //}

        //[HttpGet("groups/users/{userId}")]
        //public ActionResult<List<DiaryController.StudentResultResponse>> GetByUserId(int userId)
        //{
        //    var userId = _securityService.FetchUserId(HttpContext.User.Claims);
        //    var student = _studentRepository.Get(s => s.UserId == userId).FirstOrDefault();
        //    var teacher = _teacherRepository.Get(t => t.UserId == userId).FirstOrDefault();

        //    if (teacher != null)
        //    {
        //        var subjects = _groupRepository.GetWithInclude(g => g.Id == teacher.)
        //    }

        //    return
        //        _diaryService.GetStudentResults(userId)
        //            .Select(result => new DiaryController.StudentResultResponse(result))
        //            .ToList();
        //}

        //public class SubjectResponse
        //{
        //    public int SubjectId { get; set; }
        //    public string Name { get; set; }

        //    public 
        //}
    }
}
