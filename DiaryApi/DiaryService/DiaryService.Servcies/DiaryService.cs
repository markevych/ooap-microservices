using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;

namespace DiaryService.Services
{
    public interface IDiaryService
    {
        List<StudentResult> GetStudentResults(int userId);
        StudentResult CreateUserResult(int studentId, int subjectId, Teacher teacher, int? point, bool wasPresent);
        StudentResult UpdateUserResult(int studentResultId, int? point, bool wasPresent);
    }

    public class DiaryService : IDiaryService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentResultRepository _studentResultRepository;

        public DiaryService(
            IStudentRepository studentRepository,
            IStudentResultRepository studentResultRepository)
        {
            _studentRepository = studentRepository;
            _studentResultRepository = studentResultRepository;
        }

        public List<StudentResult> GetStudentResults(int userId)
        {
            var st = _studentRepository.Get();
            var student = _studentRepository
                .Get(s => s.UserId == userId)
                .FirstOrDefault();
            if (student == null)
                throw new ArgumentOutOfRangeException();

            return _studentResultRepository
                .GetWithInclude(sr => sr.StudentId == student.Id, sr => sr.TeacherSubject)
                .ToList();
        }

        public StudentResult CreateUserResult(int studentId, int subjectId, Teacher teacher, int? point, bool wasPresent)
        {
            var student = _studentRepository.GetWithInclude(
                s => s.Id == studentId,
                s => s.Group.TeacherSubjects, s => s.StudentResults)
                .FirstOrDefault();
            if (student == null)
                throw new ArgumentOutOfRangeException();

            var teacherSubject =
                student.Group.TeacherSubjects.FirstOrDefault(ts => ts.TeacherId == teacher.Id && ts.SubjectId == subjectId);
            if (teacherSubject == null)
                throw new ArgumentOutOfRangeException();

            var studentResult = _studentResultRepository.Create(new StudentResult
            {
                Point = wasPresent ? point : null,
                WasPresent = wasPresent,
                StudentId = studentId,
                TeacherSubjectId = teacherSubject.Id
            });

            studentResult.TeacherSubject = teacherSubject;
            studentResult.Student = student;

            return studentResult;
        }

        public StudentResult UpdateUserResult(int studentResultId, int? point, bool wasPresent)
        {
            var studentResult = _studentResultRepository
                .GetWithInclude(sr => sr.Id == studentResultId, sr => sr.Student, sr => sr.TeacherSubject)
                .FirstOrDefault();
            if (studentResult == null)
                throw new ArgumentOutOfRangeException();

            studentResult.Point = wasPresent ? point : null;
            studentResult.WasPresent = wasPresent;

            _studentResultRepository.Update(studentResult);

            return studentResult;
        }
    }
}
