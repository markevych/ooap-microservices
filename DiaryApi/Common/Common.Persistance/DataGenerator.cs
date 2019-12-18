using System;
using System.Collections.Generic;
using Common.Domain.Enums;
using Common.Domain.Models;
using Common.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Persistence
{
    [Obsolete]
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>());
            if (context.Users.Any())
            {
                return;   // Data was already seeded
            }

            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Email = "klymenkowolodymyr@gmail.com",
                    FullName = "Volodymyr Klymenko",
                    PasswordHash = "password",
                    UserRole = UserRole.Student
                },
                new User
                {
                    Id = 2,
                    Email = "test@gmail.com",
                    FullName = "Test Test",
                    PasswordHash = "password",
                    UserRole = UserRole.Teacher
                },
                new User
                {
                    Id = 3,
                    Email = "markiyan@gmail.com",
                    FullName = "Mark",
                    PasswordHash = "1111",
                    UserRole = UserRole.SuperAdmin
                });

            context.SaveChanges();


            var group = new Group
            {
                Id = 1,
                GroupName = "Pmi53",
                Students = new List<Student>(),
                TeacherSubjects = new List<TeacherSubject>()
            };

            var student = new Student
            {
                Group = group,
                Id = 1,
                UserId = 1,
                GroupId = 1,
                ParentEmail = "secret@gmail.com",
                StudentResults = new List<StudentResult>()
            };

            group.Students.Add(student);

            var teacher = new Teacher
            {
                Id = 1,
                UserId = 2,
                TeacherSubjects = new List<TeacherSubject>()
            };

            var subject = new Subject
            {
                Id = 1,
                Description = "Mathematics analysis introduction",
                Name = "Matan",
                TeacherSubjects = new List<TeacherSubject>()
            };

            var teacherSubject = new TeacherSubject
            {
                Group = group,
                GroupId = group.Id,
                Id = 1,
                StudentResults = new List<StudentResult>(),
                Subject = subject,
                SubjectId = subject.Id,
                Teacher = teacher,
                TeacherId = teacher.Id
            };

            subject.TeacherSubjects.Add(teacherSubject);
            teacher.TeacherSubjects.Add(teacherSubject);
            group.TeacherSubjects.Add(teacherSubject);

            context.Subjects.Add(subject);
            context.Groups.Add(group);
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.TeacherSubjects.Add(teacherSubject);
            
            context.SaveChanges();
        }
    }
}
