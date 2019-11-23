using System;
using System.Linq;
using Common.Domain.Models;
using Common.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                //3. Get the instance of BoardGamesDBContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationContext>();

                //4. Call the DataGenerator to create sample data
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
                    PasswordHash = "password"
                },
                new User
                {
                    Id = 2,
                    Email = "test@gmail.com",
                    FullName = "Test Test",
                    PasswordHash = "password"
                });

            context.SaveChanges();
        }
    }
}
