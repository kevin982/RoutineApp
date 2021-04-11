using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RoutineApp.Mappers.Classes;
using RoutineApp.Mappers.Interfaces;
using RoutineApp.Services.Classes;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IWorkOutService, WorkOutService>();

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IImageMapper, ImageMapper>();
            services.AddScoped<IExerciseMapper, ExerciseMapper>();
            
            return services;
        }
    }
}
