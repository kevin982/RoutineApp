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

            #region Database

            services.AddDbContext<ExerciseMsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => 
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly("ExerciseMS_API");
                });
                
            });

            #endregion

            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICustomSender, CustomSender>();

            #endregion

            #region UoW

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Authentication and Authorization

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:9002";

                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = false
                    };

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ExerciseScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "exerciseMs.all");
                });
            });
            
            #endregion

            return services;
        }
    }
}
