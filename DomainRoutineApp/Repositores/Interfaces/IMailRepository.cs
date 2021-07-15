using DomainRoutineApp.Models.Requests.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Repositores.Interfaces
{
    public interface IMailRepository
    {
        Task<string> GetMailHmlAsyncByName(GetMailHtmlRequest model);
    }
}
