using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Account
{
    public class ChangePasswordRequestModel
    {
        [Required(ErrorMessage = "You must enter your old password"), Display(Name = "Please enter your old password"), DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "You must enter your new password"), Display(Name = "Please enter your new password"), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "You must confirm your new password"), Display(Name = "Please confirm your new password"), DataType(DataType.Password), Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
