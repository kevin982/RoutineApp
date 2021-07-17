using DomainRoutineApp.Models.Requests.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string PathHtmlMail, List<string> emailList, List<Attachment> attachments, List<(string, string)> values);
 
 
    }
}
