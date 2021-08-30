using ExerciseMS_Core.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Models.Requests
{
    public class CreateExerciseRequest
    {
        [Required(ErrorMessage = "You must enter the exercise name"), Display(Name = "Please enter the exercise name"), MaxLength(30), MinLength(5)]
        public string Name { get; set; }

        public Category Category { get; set; } 

        [Required(ErrorMessage = "You must choose an image")]
        public Stream Image { get; set; }

        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
    }
}
