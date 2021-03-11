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
        private readonly UserManager<User> _userManager = null;
        private readonly SignInManager<User> _signInManager = null;
        private readonly IEmailService _emailService = null;
        private readonly IUserService _userService = null;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,IEmailService emailService, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _userService = userService;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel model)
        {

            User user = model.Clone() as User;
            user.UserName = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) 
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendEmailConfirmationAsync(user, token);
            }

            return result;
        }

        private async Task SendEmailConfirmationAsync(User user, string token)
        {

            string htmlPath = "C:\\Users\\admin\\Desktop\\Programming practices\\C#\\Back\\MVC\\RoutineApp\\RoutineApp\\Mails\\ConfirmEmail.html";
            string subject = "Email Confirmation";
            List<Attachment> attachments = new();
            List<string> mails = new() { user.Email };
            List<(string, string)> values = new() { ("Link", string.Format("https://localhost:44384" + $"/Account/ConfirmEmail?id={user.Id}&token={token}")), ("UserName", user.FirstName) };

            await _emailService.SendEmailAsync(subject, htmlPath, mails, attachments, values);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string id, string token)
        {

            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }

        public async Task<SignInResult> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            User user = await _userManager.FindByIdAsync(_userService.GetUserId());

            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        public async Task<string> SendEmailToResetPasswordAsync(EmailResetPasswordModel model)
        {
            User user = null;

            user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return "You have not created an account";

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string subject = "Reset Password";
            string htmlPath = "C:\\Users\\admin\\Desktop\\Programming practices\\C#\\Back\\MVC\\RoutineApp\\RoutineApp\\Mails\\ResetPassword.html";
            List<string> mails = new() { model.Email };
            List<Attachment> attachments = new();
            List<(string, string)> values = new() {("Link", string.Format("https://localhost:44384" + $"/Account/ResetPassword?id={user.Id}&token={token}")), ("UserName", user.FirstName) };

            await _emailService.SendEmailAsync(subject, htmlPath, mails, attachments, values);

            return "Check your email to reset your password";
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);

            return await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        }

    }
}
