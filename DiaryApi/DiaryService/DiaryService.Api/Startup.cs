using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Common.Auth;
using Common.Domain.Interfaces.Persistence;
using Common.Domain.Interfaces.Security;
using Common.Infrastructure.Swagger;
using Common.Persistence.Contexts;
using Common.Persistence.Repositories;
using DiaryService.Services;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Api
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
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Diary API", Version = "v1" });

                options.AddJwtBearerSecurityHeaderOptions();
                options.DescribeAllParametersInCamelCase();
            });

            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "ApplicationDb"));

            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IStudentResultRepository, StudentResultRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            services.AddTransient<IDiaryService, Services.DiaryService>();
            services.AddTransient<ISecurityService, SecurityService>();

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

            // app.UseHttpsRedirection();

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
