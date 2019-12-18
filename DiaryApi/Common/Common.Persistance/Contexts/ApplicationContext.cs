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
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany<TeacherSubject>(g => g.TeacherSubjects)
                .WithOne(ts => ts.Group)
                .HasForeignKey(ts => ts.GroupId);

            modelBuilder.Entity<Group>()
                .HasMany<Student>(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            modelBuilder.Entity<Student>()
                .HasMany<StudentResult>(sr => sr.StudentResults)
                .WithOne(s => s.Student)
                .HasForeignKey(sr => sr.StudentId);

            modelBuilder.Entity<StudentResult>()
                .HasOne<TeacherSubject>(sr => sr.TeacherSubject)
                .WithMany(ts => ts.StudentResults)
                .HasForeignKey(sr => sr.TeacherSubjectId);

            modelBuilder.Entity<StudentResult>()
                .HasOne<Student>(sr => sr.Student)
                .WithMany(s => s.StudentResults)
                .HasForeignKey(sr => sr.StudentId);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.TeacherSubjects)
                .WithOne(ts => ts.Subject)
                .HasForeignKey(ts => ts.SubjectId);

            modelBuilder.Entity<Teacher>()
                .HasMany(s => s.TeacherSubjects)
                .WithOne(ts => ts.Teacher)
                .HasForeignKey(ts => ts.TeacherId);

            modelBuilder.Entity<TeacherSubject>()
                .HasMany(ts => ts.StudentResults)
                .WithOne(sr => sr.TeacherSubject)
                .HasForeignKey(sr => sr.TeacherSubjectId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne<Group>(ts => ts.Group)
                .WithMany(g => g.TeacherSubjects)
                .HasForeignKey(ts => ts.GroupId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne<Teacher>(ts => ts.Teacher)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(ts => ts.TeacherId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne<Subject>(ts => ts.Subject)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(s => s.SubjectId);
        }
    }
}
