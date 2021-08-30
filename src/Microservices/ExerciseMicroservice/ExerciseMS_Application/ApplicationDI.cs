using ExerciseMS_Application.Mappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICategoryMapper, CategoryMapper>();
            services.AddScoped<IExerciseMapper, ExerciseMapper>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
