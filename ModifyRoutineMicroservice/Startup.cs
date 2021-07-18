using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DomainRoutineLibrary;
using DomainRoutineLibrary.Entities;
using DomainRoutineLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModifyRoutineMicroservice.Repository;
using ModifyRoutineMicroservice.Services;
 
namespace ModifyRoutineMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ModifyRoutineMicroservice", Version = "v1" });
            });
            #endregion

            #region Authentication

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            #endregion

            #region Authorization

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ModifyRoutineScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "modifyroutineapi");
                });
            });

            #endregion

            #region Database

            services.AddDbContext<RoutineContext>(options =>
            {
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
            });


            #endregion


            #region CORSPolicies
            services.AddCors(options =>
                    options.AddPolicy("MVCRoutineClient", builder =>
                    {
                        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .WithMethods("DELETE, PATCH")
                        .AllowAnyHeader();
                    })
                );
            #endregion

            #region Https

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                options.HttpsPort = 443;
            });

            #endregion
 

            #region ServicesAndRepositories
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IModifyRoutineRepository, ModifyRoutineRepository>();
            services.AddScoped<IModifyRoutineService, ModifyRoutineService>();
 
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ModifyRoutineMicroservice v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();    

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireAuthorization("ModifyRoutineScope"); ;
            });
        }
    }
}
