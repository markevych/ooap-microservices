using System.Collections.Generic;

using Common.Domain.Models;


namespace AdministrationService.Services.Interfaces
{
    public interface IAdministrationDiaryService
    {
        void CreateSubjectForGroup(int groupId, int subjectId, int teacherId);
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
