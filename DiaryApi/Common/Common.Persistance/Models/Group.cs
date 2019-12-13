using System.Collections.Generic;

namespace Common.Persistence.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public List<Subject> Subjects { get; set; }
        public List<GroupSubject> GroupSubject { get; set; }
    }
}
