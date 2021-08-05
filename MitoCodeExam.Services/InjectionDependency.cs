using Microsoft.Extensions.DependencyInjection;
using MitoCodeExam.DataAccess.Interfaces;
using MitoCodeExam.DataAccess.Repositories;
using MitoCodeExam.Services.Implementations;
using MitoCodeExam.Services.Interfaces;

namespace MitoCodeExam.Services
{
   public static class InjectionDependency
   {
      public static IServiceCollection AddInjection(this IServiceCollection services)
      {
         services.AddTransient<ICategoryRepository, CategoryRepository>();
         services.AddTransient<ICategoryService, CategoryService>();

         return services;
      }
   }
}
