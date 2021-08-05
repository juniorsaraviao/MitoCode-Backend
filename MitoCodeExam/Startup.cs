using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MitoCodeExam.DataAccess;
using MitoCodeExam.Services;
using System;

namespace MitoCodeExam
{
   public class Startup
   {
      public Startup(IWebHostEnvironment environment)
      {
         var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

         builder.AddEnvironmentVariables();

         Configuration = builder.Build();
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddApiVersioning( options => {
            options.ReportApiVersions                   = true;
            options.DefaultApiVersion                   = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
         } );

         services.AddInjection();

         services.AddDbContext<MitoCodeExamDbContext>(options =>
         {
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
            options.LogTo(Console.WriteLine, LogLevel.Information);
         });

         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MitoCodeExam", Version = "v1" });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MitoCodeExam v1"));
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
