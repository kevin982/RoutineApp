using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Application.Validations.Commands;
using ExerciseMS_Application.Validations.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExerciseMS_Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICategoryMapper, CategoryMapper>();
            services.AddScoped<IExerciseMapper, ExerciseMapper>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Validators

            services.AddScoped<IValidator<CreateCategoryCommand>,CreateCategoryCommandValidator>();
            services.AddScoped<IValidator<CreateExerciseCommand>, CreateExerciseCommandValidator>();
            services.AddScoped<IValidator<DeleteCategoryCommand>,DeleteCategoryCommandValidator>();
            services.AddScoped<IValidator<DeleteExerciseCommand>,DeleteExerciseCommandValidator>();
            services.AddScoped<IValidator<GetExercisesByCategoryQuery>,GetExercisesByCategoryQueryValidator>();
            services.AddScoped<IValidator<GetExercisesCountByCategoryQuery>,GetExercisesCountByCategoryQueryValidator>();

            #endregion



            return services;
        }
    }
}
