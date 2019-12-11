using System;
using System.Linq;
using Common.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Common.Domain.Models;
using Common.Persistence.Contexts;

namespace IdentityService.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationContext>();

                DataGenerator.Initialize(services);
            }

            try
            {
                logger.LogInformation("Stating hosting identity service");

                host.Run();

                logger.LogInformation("Finish hosting identity service");
                return 0;
            }
            catch (Exception e)
            {
                logger.LogError($"Cannot start hosting identity service, {e.Message}");
                return -1;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                });
    }

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
