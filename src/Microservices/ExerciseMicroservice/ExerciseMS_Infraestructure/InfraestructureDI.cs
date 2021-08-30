using ExerciseMS_Core.Services;
using ExerciseMS_Core.UoW;
using ExerciseMS_Infraestructure.Data;
using ExerciseMS_Infraestructure.Services;
using ExerciseMS_Infraestructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure
{
    public static class InfraestructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            

            services.AddDbContext<ExerciseMsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => 
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly("ExerciseMS_API");
                });
                
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();



            return services;
        }
    }
}
