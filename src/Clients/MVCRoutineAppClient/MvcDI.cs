using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MVCRoutineAppClient.Filters;
using MVCRoutineAppClient.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient
{
    public static class MvcDI
    {
        public static IServiceCollection AddMvcClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMyAuthentication(Configuration);

            services.AddOcelotClient();

            services.AddMyOwnServices();

            return services;
        }

        private static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options => 
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options => 
                {
                    options.ClientId = Configuration["Oidc:ClientId"];
                    options.Authority = Configuration["Oidc:Authority"];
                    options.ClientSecret = Configuration["Oidc:Secret"];
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.SaveTokens = true;

                    options.Scope.Add("offline_access");
                    options.Scope.Add("profile");
                    options.Scope.Add("role");
                    options.Scope.Add("categoryMs.all");
                    options.Scope.Add("exerciseMs.all");
                    options.Scope.Add("routineMs.all");
                    options.Scope.Add("statistisMs.all");

                    options.ClaimActions.MapUniqueJsonKey("role", "role");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "role"
                    };
 
                    options.ClaimActions.MapUniqueJsonKey("role", "role");

                });

            return services;
        }
 
        private static IServiceCollection AddOcelotClient(this IServiceCollection services)
        {
            services.AddHttpClient("Ocelot", options => 
            {
                options.BaseAddress = new Uri("https://localhost:9001");
            });

            return services;
        }

        private static IServiceCollection AddMyOwnServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IExerciseService, ExerciseService>();

            return services;
        }
    }
}
