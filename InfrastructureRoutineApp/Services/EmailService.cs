
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

namespace InfrastructureRoutineApp.Services
{
    public class EmailService:IEmailService
    {
        private readonly IConfiguration _configuration = null;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string subject, string PathHtmlMail, List<string> emailList, List<Attachment> attachments, List<(string, string)> values)
        {
            MailMessage mail = new()
            {
                Subject = subject,
                Body = await File.ReadAllTextAsync(PathHtmlMail),
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
                Credentials = new NetworkCredential(_configuration.GetValue<string>("SMTPConfig:EmailSender"), _configuration.GetValue<string>("SMTPConfig:Password"))
            };

            await smtpClient.SendMailAsync(mail);

        }
    }
}
