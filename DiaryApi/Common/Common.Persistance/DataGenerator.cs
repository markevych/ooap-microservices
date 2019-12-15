using System;
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
        }
    }
}
