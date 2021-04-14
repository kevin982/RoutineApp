using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Account
{
    public class SignInRequestModel
    {
        [Required, Display(Name = "Please enter your email"), EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Please enter your password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Choose if you want to be remembered the next time")]
        public bool RememberMe { get; set; }
    }
}
