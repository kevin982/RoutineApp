using Microsoft.AspNetCore.Identity;
using RoutineApp.Data.Entities;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<UserService> _userManager = null;
        private readonly SignInManager<UserService> _signInManager = null;
        private readonly IEmailService _emailService = null;


        public AccountService(UserManager<UserService> userManager, SignInManager<UserService> signInManager,IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel model)
        {

            UserService user = model.Clone() as UserService;
            user.UserName = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) 
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendEmailConfirmationAsync(user, token);
            }

            return result;
        }

        private async Task SendEmailConfirmationAsync(UserService user, string token)
        {
            string htmlPath = "C:\\Users\\admin\\Desktop\\Programming practices\\C#\\Back\\RoutineApp\\RoutineApp\\Mails\\ConfirmEmail.html";
            string subject = "Email Confirmation";
            List<Attachment> attachments = new();
            List<string> mails = new() { user.Email };
            List<(string, string)> values = new() { ("Link", string.Format("http:localhost:44384" + $"/Account/ConfirmEmail?id={user.Id}&token={token}")), ("UserName", user.FirstName) };

            await _emailService.SendEmailAsync(subject, htmlPath, mails, attachments, values);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string id, string token)
        {
            token.Replace(" ", "+");

            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }

        public async Task<SignInResult> SignIn(SignInModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
        }
    }
}
