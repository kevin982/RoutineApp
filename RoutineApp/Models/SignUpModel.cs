﻿using RoutineApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class SignUpModel:ICloneable
    {
        [Required(ErrorMessage= "You must enter your name"), Display(Description = "Enter your name"), MaxLength(20), MinLength(4)]
        public string FistName { get; set; } = string.Empty;

        [Required(ErrorMessage= "You must enter your last name"), Display(Description = "Enter your last name"), MaxLength(20), MinLength(4)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage= "You must enter your email"), Display(Description = "Enter your email"), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage= "You must enter your age"), Display(Description = "Enter your age"), Range(17,100)]
        public int Age { get; set; } = 0;

        [Required(ErrorMessage= "You must enter a password"), Display(Description = "Enter the password"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must confirm your password"), Display(Description = "Confirm your password"), DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public object Clone()
        {
            User user = new()
            {
                FirstName = this.FistName,
                LastName = this.LastName,
                Email = this.Email,
                Age = this.Age
            };
            return user;
        }
    }
}
