using CategoryMS_Application.Commands;
using CategoryMS_Application.Validations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Validators

            services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            
            #endregion



            return services;
        }
    }
}
