using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoutineMS_Core.Services;
using RoutineMS_Core.UoW;
using RoutineMS_Infraestructure.Data;
using RoutineMS_Infraestructure.Services;
using RoutineMS_Infraestructure.UoW;
using System;

namespace RoutineMS_Infraestructure
{
    public static class InfraestructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RoutineMsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly("RoutineMS_API");
                });

            });


            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomSender, CustomSender>();
            services.AddScoped<IPublisherService, PublisherService>();

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
                options.AddPolicy("RoutineScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "routineMs.all");
                });
            });

            #endregion

            return services;
        }
    }
}
