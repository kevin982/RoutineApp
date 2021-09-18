using CategoryMS_Core.Services;
using CategoryMS_Core.UoW;
using CategoryMS_Infraestructure.Data;
using CategoryMS_Infraestructure.Services;
using CategoryMS_Infraestructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Infraestructure
{
    public static class InfraestructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region Database

            services.AddDbContext<CategoryMsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly("CategoryMS_API");
                });

            });

            #endregion

            #region Services

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
                options.AddPolicy("CategoryScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "categoryMs.all");
                });
            });

            #endregion

            return services;
        }
    }
}
