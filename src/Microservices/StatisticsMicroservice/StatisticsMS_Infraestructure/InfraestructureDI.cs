using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatisticsMS_Core.Services;
using StatisticsMS_Core.UoW;
using StatisticsMS_Infraestructure.Data;
using StatisticsMS_Infraestructure.Services;
using StatisticsMS_Infraestructure.UoW;
using System;

namespace StatisticsMS_Infraestructure
{
    public static class InfraestructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region Database

            services.AddDbContext<StatisticsMSContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly("StatisticsMS_API");
                });

            });

            #endregion
            
            #region Services

            services.AddScoped<IUserService, UserService>();
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
                options.AddPolicy("StatisticsScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "statisticsMs.all");
                });
            });

            #endregion

            return services;
        }
    }
}
