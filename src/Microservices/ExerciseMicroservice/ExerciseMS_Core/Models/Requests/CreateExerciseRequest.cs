﻿using Microsoft.AspNetCore.Http;
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
        [Required(ErrorMessage = "You must enter the exercise name"), Display(Name = "Please enter the exercise name"), MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Please choose the category")]
        public int Category { get; set; } = new();

        [Display(Name = "Choose the exercise image"), Required(ErrorMessage = "You must choose an image")]
        public Stream Image { get; set; }
    }
}
