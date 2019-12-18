using System;
using System.Collections.Generic;

using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using AdministrationService.Services.Interfaces;


namespace AdministrationService.Services.Services
{
    public class AdministrationDiaryService : IAdministrationDiaryService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AdministrationDiaryService(
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        public void CreateSubjectForGroup(int groupId, int subjectId, int teacherId)
        {
            var subject = _subjectRepository.FindById(subjectId);
            if (subject == null)
                throw new ArgumentOutOfRangeException();

            var teacher = _teacherRepository.FindById(subjectId);
            if (teacher == null)
                throw new ArgumentOutOfRangeException();

            var group = _groupRepository.FindById(groupId);
            if (group.TeacherSubjects == null)
            {
                group.TeacherSubjects = new List<TeacherSubject>();
            }

            group.TeacherSubjects.Add(new TeacherSubject{TeacherId = teacherId, SubjectId = subjectId, GroupId = groupId});
            _groupRepository.Update(group);
        }
        public void RemoveSubject(int subjectId)
        {
            var subject = _subjectRepository.FindById(subjectId);

            _subjectRepository.Remove(subject);
        }
        public Subject GetSubject(int subjectId)
        {
            var subject = _subjectRepository.FindById(subjectId);

            return subject;
        }
        public void UpdateSubject(int subjectId, Subject updatedSubject)
        {
            var subject = _subjectRepository.FindById(subjectId);

            subject.Name = updatedSubject.Name;
            subject.Description = updatedSubject.Description;

            _subjectRepository.Update(subject);
        }
        public IEnumerable<Subject> GetSubjects()
        {
            var subjects = _subjectRepository.Get();

            return subjects;
        }
        public Group GetGroup(int groupId)
        {
            var group = _groupRepository.FindById(groupId);

            return group;
        }
        public void UpdateGroup(int groupId, Group updatedGroup)
        {
            var group = _groupRepository.FindById(groupId);

            group.GroupName = updatedGroup.GroupName;

            _groupRepository.Update(group);
        }
        public void RemoveGroup(int groupId)
        {
            var group = _groupRepository.FindById(groupId);

            _groupRepository.Remove(group);
        }
        public IEnumerable<Group> GetGroups()
        {
            var groups = _groupRepository.Get();

            return groups;
        }
    }
}
