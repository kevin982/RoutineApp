using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Mail;
using DomainRoutineApp.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class MailRepository : IMailRepository
    {
        private readonly RoutineContext _context;

        public MailRepository(RoutineContext context)
        {
            _context = context;
        }

        public async Task<string> GetMailHmlAsyncByName(GetMailHtmlRequest model)
        {
            
            if ((model is null) || (model.MailName != "ConfirmEmail" && model.MailName != "ResetPassword")) throw new Exception("This mail name is not valid");


            string html = await _context.Mails
                .AsNoTracking()
                .Where(m => m.MailName == model.MailName)
                .Select(m => m.MailHtmlCode)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(html)) return html;

            await SeedDefaultMailsTemplates();

            html = await _context.Mails
                .AsNoTracking()
                .Where(m => m.MailName == model.MailName)
                .Select(m => m.MailHtmlCode)
                .FirstOrDefaultAsync();

            return html;
        }

        private async Task SeedDefaultMailsTemplates()
        {
            string confirmEmail = @"<!DOCTYPE html><html><head><meta charset=""utf - 8"" />< title ></ title ></ head >< body >< h1 > Hi { { UserName} }</ h1 >< p > We are really happy for your subscription to our app, please click the following link to confirm your email adress.</ p >< br />< a href = ""{{Link}}"" > Click here! </ a ></ body ></ html > ";

            string resetPassword = @"<!DOCTYPE html><html><head><meta charset=""utf - 8"" />< title ></ title ></ head < body >< h1 > Reset your password</ h1 >< p > Hi { { UserName} }, please click the following link to reset your password </ p >< a href = ""{{Link}}"" > Click here! </ a ></ body ></ html > ";

            await _context.Mails.AddAsync(new Mails { MailName = "ConfirmEmail", MailHtmlCode = confirmEmail });
            await _context.Mails.AddAsync(new Mails { MailName = "ResetPassword", MailHtmlCode = resetPassword});

            await _context.SaveChangesAsync();
        }

    }
}
