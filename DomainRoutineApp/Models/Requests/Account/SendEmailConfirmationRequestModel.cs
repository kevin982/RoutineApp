using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Account
{
    public class SendEmailConfirmationRequestModel
    {
        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
    }
}
