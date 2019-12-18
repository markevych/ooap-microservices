using System.Collections.Generic;

namespace Common.Domain.Models
{
    public class TeacherSubject
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<StudentResult> StudentResults { get; set; }
    }
}
