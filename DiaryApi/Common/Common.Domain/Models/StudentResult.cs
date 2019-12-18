namespace Common.Domain.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        public int? Point { get; set; }
        public bool WasPresent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeacherSubjectId { get; set; }
        public TeacherSubject TeacherSubject { get; set; }
    }
}
