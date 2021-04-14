using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Account
{
    public class ConfirmEmailRequestModel
    {
        public string Token { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
    }
}
