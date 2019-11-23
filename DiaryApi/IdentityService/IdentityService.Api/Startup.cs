using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Common.Auth;
using Common.Domain.Interfaces.Persistence;
using Common.Persistence.Contexts;
using Common.Persistence.Repositories;
using IdentityService.Services.Interfaces;
using IdentityService.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service API", Version = "v1" });

                options.DescribeAllEnumsAsStrings();
                options.DescribeAllParametersInCamelCase();
            });

            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "ApplicationDb"));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<ISecurityService, SecurityService>();

            services.AddJwtAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
