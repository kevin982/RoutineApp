using IdentityMicroservice.Mappers;
using IdentityMicroservice.Models;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMicroservice.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly IConfiguration Configuration = null;
        private readonly IAccountMapper _accountMapper = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly RoleManager<IdentityRole> _roleManager = null;




        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IAccountMapper accountMapper, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            Configuration = configuration;
            _accountMapper = accountMapper;
            _httpContext = httpContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.UserId);

                if (user is null) throw new Exception("The user could no be found");

                if (user.UsedExternalProvider) throw new Exception("Could not change password since you used an external identity provider!");

                return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IdentityResult> ConfirmEmailAsync(string token, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user is null) throw new Exception("The user could no be found");

                if (user.UsedExternalProvider) throw new Exception("Could not confirm your email since you used an external identity provider!");

                var result = await _userManager.ConfirmEmailAsync(user, token);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpRequestModel model)
        {
            try
            {
                var user = _accountMapper.MapSignUpRequestModelToDomain(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded) throw new Exception(result.Errors.ToList()[0].Description);

                await _userManager.AddToRoleAsync(user, "user");

                if (model.Email == Configuration["AdminEmail"]) await _userManager.AddToRoleAsync(user, "admin");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                await SendEmailConfirmationAsync(token, user.Email, user.Id, user.UserName);

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordRequestModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user is null) throw new Exception("The user could no be found");

                if (user.UsedExternalProvider) throw new Exception("Could not reset password since you used an external identity provider!");

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (!result.Succeeded) throw new Exception(result.Errors.ToList()[0].Description);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendEmailToResetPasswordAsync(EmailResetPasswordRequestModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.UserEmail);

                if (user is null) throw new Exception("The user could no be found");

                if (user.UsedExternalProvider) throw new Exception("Could not send email to reset password since you used an external identity provider!");


                var token = await _userManager.GeneratePasswordResetTokenAsync(user);


                string htmlparent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

                string htmlPath = htmlparent += "\\IdentityMicroservice\\Mails\\ResetPassword.html";

                string subject = "Reset Password";
                List<Attachment> attachments = new();
                List<string> mails = new() { model.UserEmail };
                List<(string, string)> values = new() { ("Link", string.Format(Configuration.GetValue<string>("AppsUrls:Self") + $"/Account/ResetPassword?id={user.Id}&token={token}")), ("UserName", user.UserName) };
                await SendEmailAsync(subject, htmlPath, mails, attachments, values);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SendEmailConfirmationAsync(string token, string email, string userId, string name)
        {
            try
            {
                string htmlparent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

                string htmlFile = htmlparent += "\\IdentityMicroservice\\Mails\\ConfirmEmail.html";

                string subject = "Email Confirmation";
                List<Attachment> attachments = new();
                List<string> mails = new() { email };
                List<(string, string)> values = new() { ("Link", string.Format(Configuration.GetValue<string>("AppsUrls:Self") + $"/Account/ConfirmEmail?id={userId}&token={token}")), ("UserName", name) };
                await SendEmailAsync(subject, htmlFile, mails, attachments, values);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task SendEmailAsync(string subject, string htmlPath, List<string> mails, List<Attachment> attachments, List<(string, string)> values)
        {

            try
            {
                string html = await File.ReadAllTextAsync(htmlPath);

                MailMessage mail = new()
                {
                    Subject = subject,
                    Body = html,
                    From = new MailAddress(Configuration.GetValue<string>("SMTPConfig:EmailSender"), Configuration.GetValue<string>("SMTPConfig:DisplayName")),
                    IsBodyHtml = Configuration.GetValue<bool>("SMTPConfig:IsBodyHTML")
                };

                foreach (var emailAddress in mails) mail.To.Add(emailAddress);
                foreach (var attchment in attachments) mail.Attachments.Add(attchment);
                foreach (var value in values) mail.Body = mail.Body.Replace($"{{{{{value.Item1}}}}}", value.Item2);

                SmtpClient smtpClient = new()
                {
                    Host = Configuration.GetValue<string>("SMTPConfig:Host"),
                    Port = Configuration.GetValue<int>("SMTPConfig:Port"),
                    EnableSsl = Configuration.GetValue<bool>("SMTPConfig:EnableSSL"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("SMTPConfig:EmailSender"), Configuration.GetValue<string>("SMTPConfig:Password"))
                };

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
