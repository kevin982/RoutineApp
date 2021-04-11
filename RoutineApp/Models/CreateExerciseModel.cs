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
        public int Category { get; set; }

        [Display(Name = "Please enter the exercise images"), Required(ErrorMessage ="You must upload at least one image")]
        public IFormFileCollection Images { get; set; } 

        public List<Image> ImageToStore { get; set; } = new();

        public List<string> ImagesUrl { get; set; } = new();


        public object Clone()
        {
            return new Exercise()
            {
                Name = this.Name,
                CategoryId = Category,
                Images = ImageToStore
            };
        }
    }
}
