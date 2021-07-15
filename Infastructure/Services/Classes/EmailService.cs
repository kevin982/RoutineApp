using DomainRoutineApp.Models.Requests.Mail;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services.Classes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration = null;
        private readonly IMailRepository _mailRepository = null;

        public EmailService(IConfiguration configuration, IMailRepository mailRepository)
        {
            _configuration = configuration;
            _mailRepository = mailRepository;
        }

        public async Task<string> GetMailHtmlAsync(GetMailHtmlRequest model)
        {
            if ((model is null) || (model.MailName != "ConfirmEmail" && model.MailName != "ResetPassword")) throw new Exception("The model to get the mail html code is not valid");

            string html = "";
            
            try
            {
                html = await _mailRepository.GetMailHmlAsyncByName(model);
                return html;
            }catch (Exception ex)
            {
                throw new Exception("The model to get the mail html code is not valid");
            }
        }
        public async Task SendEmailAsync(string subject, string htmlPath, List<string> emailList, List<Attachment> attachments, List<(string, string)> values)
        {
            string html = await File.ReadAllTextAsync(htmlPath);

            MailMessage mail = new()
            {
                Subject = subject,
                Body = html,
                From = new MailAddress(_configuration.GetValue<string>("SMTPConfig:EmailSender"), _configuration.GetValue<string>("SMTPConfig:DisplayName")),
                IsBodyHtml = _configuration.GetValue<bool>("SMTPConfig:IsBodyHTML")
            };

            foreach (var emailAddress in emailList) mail.To.Add(emailAddress);
            foreach (var attchment in attachments) mail.Attachments.Add(attchment);
            foreach (var value in values) mail.Body = mail.Body.Replace($"{{{{{value.Item1}}}}}", value.Item2);

            SmtpClient smtpClient = new()
            {
                Host = _configuration.GetValue<string>("SMTPConfig:Host"),
                Port = _configuration.GetValue<int>("SMTPConfig:Port"),
                EnableSsl = _configuration.GetValue<bool>("SMTPConfig:EnableSSL"),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration.GetValue<string>("SMTPConfig:EmailSender"), _configuration.GetValue<string>("SMTPConfig:Password"))
            };

            try
            {
                await smtpClient.SendMailAsync(mail);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


        }
    }
}
