using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Account
{
    public class EmailResetPasswordRequestModel
    {
        public string UserEmail { get; set; } = string.Empty;
    }
}
