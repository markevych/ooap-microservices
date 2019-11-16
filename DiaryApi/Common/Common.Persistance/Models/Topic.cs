namespace Common.Persistence.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
