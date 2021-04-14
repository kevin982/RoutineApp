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

        [Display(Name = "Please enter the category")]
        public int Category { get; set; } = new();

        [Display(Name = "Please enter the exercise images"), Required(ErrorMessage = "You must upload at least one image")]
        public IFormFileCollection Images { get; set; }
        public List<string> ImagesUrl { get; set; } = new();
    }
}
