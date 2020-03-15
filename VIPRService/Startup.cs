using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using VIPRService.Extensions;
using VIPRService.Helpers;
using VIPRService.Process;

namespace VIPRService
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
            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(Configuration)              
              .WriteTo.RollingFile($"Logs/log.txt")
              .CreateLogger();

            LogHelper.Initiate(logger);

            services.AddControllers();
            services.AddMvc(s => s.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "VIPR API",
                    Description = "Using ASP.NET Core Web API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "", Email = "" }
                });
            });
            services.AddLogging(b =>
            {
                b.AddSerilog(logger);
            });
            services.Configure<AppSettings>(Configuration);
            services.AddTransient<IDataImporter, DataImporter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VIPR API V1");
            });

            //create csv folder
            var csvFolderPath = Path.Combine(env.ContentRootPath, Configuration["CSVFolderName"]);
            if (!Directory.Exists(csvFolderPath))
                Directory.CreateDirectory(csvFolderPath);
        }
    }
}
