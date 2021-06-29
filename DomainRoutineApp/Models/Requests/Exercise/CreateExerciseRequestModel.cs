using DomainRoutineApp.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Exercise
{
    public class CreateExerciseRequestModel
    {
        [Required(ErrorMessage = "You must enter the exercise name"), Display(Name = "Please enter the exercise name"), MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Please choose the category")]
        public int Category { get; set; } = new();

        [Display(Name ="Choose the exercise image"), Required(ErrorMessage = "You must choose an image")]
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
