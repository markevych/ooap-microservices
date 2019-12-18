using System.Collections.Generic;

namespace Common.Domain.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<TeacherSubject> TeacherSubjects { get; set; }
    }
}
