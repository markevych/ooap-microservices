using System;
using System.Collections.Generic;
using System.Text;

using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using AdministrationService.Services.Interfaces;


namespace AdministrationService.Services.Services
{
    public class AdministrationDiaryService : IAdministationDiaryService
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Subject> _subjectRepository;

        public AdministrationDiaryService(IRepository<Group> groupRepository, IRepository<Subject> subjectRepository)
        {
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
        }

        public void CreateSubjectForGroup(int groupId, Subject subject)
        {
            var group = _groupRepository.FindById(groupId);
            if (group.Subjects == null)
            {
                group.Subjects = new List<Subject>();
            }
            group.Subjects.Add(subject);
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
