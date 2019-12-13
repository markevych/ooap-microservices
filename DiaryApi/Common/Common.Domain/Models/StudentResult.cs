namespace Common.Domain.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public bool IsPresent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
