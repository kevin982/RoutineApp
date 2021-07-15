using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Entities
{
    public class Mails
    {
        public Guid Id { get; set; } = new Guid();

        public string MailName { get; set; } = string.Empty;

        public string MailHtmlCode { get; set; } = string.Empty;
    }
}
