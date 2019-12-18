using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using AdministrationService.Services.Interfaces;
using AdministrationService.Services.Services;
using Common.Auth;
using Common.Infrastructure.Swagger;
using Common.Persistence.Repositories;
using Common.Domain.Interfaces.Persistence;
using Common.Persistence.Contexts;

namespace AdministrationService.Api
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
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Administration API", Version = "v1" });

                options.AddJwtBearerSecurityHeaderOptions();
                options.DescribeAllParametersInCamelCase();
            });

            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "ApplicationDb"));

            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IAdministrationDiaryService, AdministrationDiaryService>();

            services.AddJwtAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}