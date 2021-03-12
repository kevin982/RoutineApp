using Microsoft.AspNetCore.Http;
using RoutineApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class CreateExerciseModel:ICloneable
    {

        [Required(ErrorMessage = "You must enter the exercise name"), Display(Name = "Please enter the exercise name"), MaxLength(30)]
        public string Name { get; set; }

        [Display(Name ="Please enter the category")]
        public string Category { get; set; }

        [Display(Name = "Please enter the exercise images")]
        public IFormFileCollection Images { get; set; } 

        public List<Image> ImageToStore { get; set; } = new();

        public object Clone()
        {
            return new Exercise()
            {
                Name = this.Name,
                Category = this.Category,
                Images = ImageToStore
            };
        }
    }
}
