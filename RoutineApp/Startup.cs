using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoutineApp.Claims;
using RoutineApp.Data;
using RoutineApp.Data.Entities;
using RoutineApp.Services.Classes;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp
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
            services.AddControllersWithViews();

            services.AddDbContext<RoutineContext>(options => options.UseSqlServer(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<RoutineContext>().AddDefaultTokenProviders();

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



                options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped<IUserService, UserService>();

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

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
