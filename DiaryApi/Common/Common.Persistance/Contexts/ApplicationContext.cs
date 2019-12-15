using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

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

            modelBuilder.Entity<Group>()
                .HasMany(g => g.GroupSubject)
                .WithOne(gs => gs.Group)
                .HasForeignKey(gr => gr.GroupId);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.GroupSubject)
                .WithOne(gs => gs.Subject)
                .HasForeignKey(gs => gs.SubjectId);

            modelBuilder.Entity<TeacherSubject>()
                .HasKey(ts => new { ts.SubjectId, ts.TeacherId });
        }
    }
}
