using System;
using System.Collections.Generic;
using System.Text;

using Common.Domain.Models;
using Common.Persistence.Models;
using Common.Persistence.Repositories;


namespace AdministrationService.Service.Interfaces
{
    public interface IAdministationDiaryService
    {
        void CreateSubjectForGroup(int groupId, Subject subject);
        void RemoveSubject(int subjectId);
        Subject GetSubject(int subjectId);
        void UpdateSubject(int subjectId, Subject updatedSubject);
        IEnumerable<Subject> GetSubjects();
        Group GetGroup(int groupId);
        void UpdateGroup(int groupId, Group updatedGroup);
        void RemoveGroup(int groupId);
        IEnumerable<Group> GetGroups();
    }
}
