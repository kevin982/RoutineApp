using ClientRoutineApp.Services.Classes;
using ClientRoutineApp.Services.Interfaces;
using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using InfrastructureRoutineApp.Mappers.Classes;
using InfrastructureRoutineApp.Repositories.Classes;
using InfrastructureRoutineApp.Services;
using InfrastructureRoutineApp.Services.Classes;
using InfrastructureRoutineApp.Validations.Services;
using Microsoft.Extensions.DependencyInjection;
using RoutineApp.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCoreApp.Extensions
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IExerciseCategoryService, ExerciseCategoryService>();
            services.AddScoped<IExerciseSetDetailsService, ExerciseSetDetailsService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IExerciseMapper, ExerciseMapper>();
            services.AddScoped<IAccountMapper, AccountMapper>();
            services.AddScoped<IExerciseDetailMapper, ExerciseDetailMapper>();
            services.AddScoped<IStatisticsMapper, StatisticsMapper>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDayRepository, DayRepository>();
            services.AddScoped<IExerciseCategoryRepository, ExerciseCategoryRepository>();
            services.AddScoped<IExerciseSetDetailsRepository, ExerciseSetDetailsRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();

            return services;
        }

        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IDayServiceValidator, DayServiceValidator>();

            return services;
        }
    }
}
