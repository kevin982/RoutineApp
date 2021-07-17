using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModifyRoutineMicroservice.Models
{
    public class AddExerciseToRoutineRequestModel
    {
        [Required(ErrorMessage = "You must provide an exercise id")]
        public int ExerciseId { get; set; } = 0;

        [Required(ErrorMessage = "You must select at least one day")]
        public List<int> Days { get; set; } = new();

        [Required(ErrorMessage = "You must enter the set number"), Range(2, 7)]
        public int Sets { get; set; } = 0;
    }
}
