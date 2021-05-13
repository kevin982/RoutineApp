using DomainRoutineApp.Models.Entities;
using InfrastructureRoutineApp;
using InfrastructureRoutineApp.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoutineCoreApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineCoreApp
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

            var builder = services.AddAuthentication();

            builder.AddFacebook(options =>
            {
                options.SaveTokens = true;
                options.ClientId = Configuration["Facebook:ClientId"];
                options.ClientSecret = Configuration["Facebook:ClientSecret"];
                options.Events.OnTicketReceived = (context) =>
                {
                    Console.WriteLine(context.HttpContext.User);
                    return Task.CompletedTask;
                };
                options.Events.OnCreatingTicket = (context) =>
                {
                    Console.WriteLine(context.Identity);
                    return Task.CompletedTask;
                };

            });

            services.AddControllersWithViews();

            

            services.AddDbContext<RoutineContext>(options =>
            {
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"), b => b.MigrationsAssembly("RoutineCoreApp"));
            });

            services.AddDataProtection()
                .SetApplicationName("RoutineAppKevinVasquezBenavides");

            services.AddIdentity<User, IdentityRole>().
                AddEntityFrameworkStores<RoutineContext>().
                AddDefaultTokenProviders().
                AddRoles<IdentityRole>();

            services.Configure<IdentityOptions>(options =>
            {
                #region Password Configuration
                options.Password.RequiredLength = Configuration.GetValue<int>("PasswordConfig:RequiredLength");
                options.Password.RequiredUniqueChars = Configuration.GetValue<int>("PasswordConfig:RequiredUniqueChars");
                options.Password.RequireDigit = Configuration.GetValue<bool>("PasswordConfig:RequireDigit");
                options.Password.RequireLowercase = Configuration.GetValue<bool>("PasswordConfig:RequireLowerCase");
                options.Password.RequireUppercase = Configuration.GetValue<bool>("PasswordConfig:RequireUpperCase");
                options.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("PasswordConfig:RequireNonAlphanumeric");
                #endregion

                #region Lockout Configuration

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                #endregion

                #region User Configurations

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;


                #endregion

                #region SignIn Configurations

                options.SignIn.RequireConfirmedEmail = true;

                #endregion

                #region LockoutConfigurations

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                options.Lockout.MaxFailedAccessAttempts = 4;

                #endregion

                #region CookiesPolicy

                services.Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;

                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });


                #endregion

            });

            services.ConfigureApplicationCookie(options =>
            {

                options.Cookie.HttpOnly = true; //Este nos sirve para establecer si solo permite http como protocolo.

                options.ExpireTimeSpan = TimeSpan.FromMinutes(5); //Este nos sirve para establecer el tiempo de vida para que expire la cookie

                options.LoginPath = "/Account/SignIn"; //Este nos sirve para establecer la ruta para hacer el loggin cuando se quiera acceder a una ruta que necesite authanticated.

                options.AccessDeniedPath = "/Account/AccessDenied";  //Este nos sirve para establecer la ruta para cuando se niegue el acceso porque no esta autorizado.

                options.SlidingExpiration = true;


            });

            services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();


            services.AddMappers();
            services.AddClientServices();
            services.AddRepositories();
            services.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseCookiePolicy();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
