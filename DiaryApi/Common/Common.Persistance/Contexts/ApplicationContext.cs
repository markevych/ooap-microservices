using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

      /*  public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSubject> GroupSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<Topic> Topics { get; set; }*/
        public DbSet<User> Users { get; set; }
    }
}
