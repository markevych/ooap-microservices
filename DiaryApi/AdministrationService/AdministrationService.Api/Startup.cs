using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Common.Auth;
using Common.Infrastructure.Swagger;
using Common.Persistence.Repositories;
using Common.Domain.Models;
using Common.Domain.Interfaces.Persistence;
using AdministrationService.Services.Interfaces;
using AdministrationService.Services.Services;

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
            services.AddTransient<IRepository<Group>, GroupRepository>();
            services.AddTransient<IRepository<Subject>, SubjectRepository>();
            services.AddTransient<IAdministationDiaryService, AdministrationDiaryService>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}