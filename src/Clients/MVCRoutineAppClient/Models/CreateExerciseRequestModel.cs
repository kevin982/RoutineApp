﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MVCRoutineAppClient.Models
{
    public class CreateExerciseRequestModel
    {
        [Required(ErrorMessage = "You must enter the exercise name"), Display(Name = "Please enter the exercise name"), MinLength(5),MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Please choose the category")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Choose the exercise image"), Required(ErrorMessage = "You must choose an image")]
        public IFormFile Image { get; set; }
        public string FileContentType { get; set; }

    }
}
