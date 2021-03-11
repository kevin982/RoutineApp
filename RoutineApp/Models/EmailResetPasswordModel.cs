using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class EmailResetPasswordModel
    {
        [Required(ErrorMessage ="You must enter your email"), Display(Name = "Please enter your mail"),EmailAddress]
        public string Email { get; set; }
    }
}
