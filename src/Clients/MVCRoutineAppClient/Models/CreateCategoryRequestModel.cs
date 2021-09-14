using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Models
{
    public class CreateCategoryRequestModel
    {
        [Required(ErrorMessage = "Please provide the category name"), MinLength(3), MaxLength(30)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
