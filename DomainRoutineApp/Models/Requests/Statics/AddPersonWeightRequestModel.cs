using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Statics
{
    public class AddPersonWeightRequestModel
    {
        [Required(ErrorMessage = "You must enter your weight")]
        [Display(Name = "Please enter your current weight")]
        public int Weight { get; set; } = 0;

        public string UserId { get; set; } = string.Empty;
    }
}
