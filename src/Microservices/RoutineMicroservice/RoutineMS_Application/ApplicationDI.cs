using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RoutineMS_Application.Commands;
using RoutineMS_Application.Validator;
using System.Reflection;

namespace RoutineMS_Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IValidator<AddExerciseToRoutineCommand>, AddExerciseValidator>();

            return services;
        }
    }
}
