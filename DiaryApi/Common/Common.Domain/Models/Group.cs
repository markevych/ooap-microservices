using System.Collections.Generic;

namespace Common.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public List<TeacherSubject> TeacherSubjects { get; set; }
        public List<Student> Students { get; set; }
    }
}
