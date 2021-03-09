using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string PathHtmlMail, List<string> emailList, List<Attachment> attachments, List<(string, string)> values);
    }
}