using Common.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSubject> GroupSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupSubject>()
                .HasKey(g => new { g.GroupId, g.SubjectId });

            modelBuilder.Entity<GroupSubject>()
                .HasOne(gs => gs.Subject)
                .WithMany(s => s.GroupSubject)
                .HasForeignKey(gs => gs.SubjectId);

            modelBuilder.Entity<GroupSubject>()
                .HasOne(gs => gs.Group)
                .WithMany(s => s.GroupSubject)
                .HasForeignKey(gs => gs.GroupId);
        }
    }
}
