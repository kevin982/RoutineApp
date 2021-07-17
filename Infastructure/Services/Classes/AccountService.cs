using DomainRoutineApp.Mappers.Interfaces;
 
using DomainRoutineApp.Models.Requests.Account;
using DomainRoutineApp.Models.Requests.Mail;
using DomainRoutineApp.Services.Interfaces;
using DomainRoutineLibrary.Entities;
using InfrastructureRoutineApp.Validations.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services.Classes
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<User> _userManager = null;
        private readonly SignInManager<User> _signInManager = null;
        private readonly IEmailService _emailService = null;
        private readonly IUserService _userService = null;
        private readonly IAccountMapper _accountMapper = null;
        private readonly IAccountValidator _accountValidator = null;
        private readonly ILogger<AccountService> _logger = null;
        

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, IUserService userService, IAccountMapper accountMapper, IAccountValidator accountValidator ,ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _userService = userService;
            _accountMapper = accountMapper;
            _accountValidator = accountValidator;
            _logger = logger;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            var validationResult = _accountValidator.ChangePasswordModelValidation(model);

            if (!validationResult.Valid) throw new Exception(validationResult.Message);

            User user = await _userManager.FindByIdAsync(_userService.GetUserId());

            if (user is null) throw new Exception("The user could no be found");

            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailRequestModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpRequestModel model)
        { 
            User user = _accountMapper.MapSignUpRequestModelToDomain(model);

            user.CreatedOn = DateTime.Now;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendEmailConfirmationAsync(
                    new SendEmailConfirmationRequestModel 
                    { 
                        Token = token,
                        Email = user.Email,
                        UserId = user.Id,
                        FirstName = user.FirstName
                    });
            }

            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordRequestModel model)
        {

            var user = await _userManager.FindByIdAsync(model.Id);

            return await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        }

        public async Task SendEmailToResetPasswordAsync(EmailResetPasswordRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserEmail);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            string htmlparent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

            string htmlFile = htmlparent += "\\RoutineMVCClientApp\\Mails\\ResetPassword.html";
 

            string htmlPath = "C:\\Users\\admin\\Desktop\\Programming practices\\C#\\Back\\MVC\\RoutineApp\\RoutineCoreApp\\Mails\\ResetPassword.html";
            string subject = "Reset Password";
            List<Attachment> attachments = new();
            List<string> mails = new() { model.UserEmail };
            List<(string, string)> values = new() { ("Link", string.Format("https://localhost:5001" + $"/Account/ResetPassword?id={user.Id}&token={token}")), ("UserName", user.FirstName) };
            await _emailService.SendEmailAsync(subject, htmlPath, mails, attachments, values);
        }

        public async Task<SignInResult> SignInAsync(SignInRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return null;

            return await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
        }

        public async Task<SignInResult> SignInFbAsync()
        {
            var result = await _signInManager.GetExternalLoginInfoAsync();

            if(result is not null)
                await _signInManager.ExternalLoginSignInAsync(result.LoginProvider, result.ProviderKey, true);

            return null;
        }



        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task SendEmailConfirmationAsync(SendEmailConfirmationRequestModel model)
        {
            string htmlparent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

            string htmlFile = htmlparent += "\\RoutineMVCClientApp\\Mails\\ConfirmEmail.html";

            string subject = "Email Confirmation";
            List<Attachment> attachments = new();
            List<string> mails = new() { model.Email };
            List<(string, string)> values = new() { ("Link", string.Format("https://localhost:5001" + $"/Account/ConfirmEmail?id={model.UserId}&token={model.Token}")), ("UserName", model.FirstName) };
            await _emailService.SendEmailAsync(subject, htmlFile, mails, attachments, values);
        }
    }
}
