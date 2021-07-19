
using IdentityServer4;

using IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DomainRoutineLibrary;
using DomainRoutineLibrary.Entities;
using System;
using Microsoft.AspNetCore.Http;
using IdentityServer.Services;
using IdentityServer.Mapper;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();


            services.AddDbContext<RoutineContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region Identity

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<RoutineContext>()
               .AddDefaultTokenProviders()
               .AddRoles<IdentityRole>();


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

            #endregion

            #region IdentityServer4

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<User>();

            #endregion


            #region MyServices

            services.AddScoped<IAccountMapper, AccountMapper>();
            services.AddScoped<IAccountService, AccountService>();

            #endregion

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}