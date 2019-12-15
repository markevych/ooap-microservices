using System.Collections.Generic;

namespace Common.Domain.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<GroupSubject> GroupSubject { get; set; }
    }
}
