using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoutineMS_Core.Models.Requests
{
    public class AddExerciseToRoutineRequest
    {
        [Required]
        public Guid ExerciseId { get; set; }

        [Required, MinLength(1)]
        public IEnumerable<int> Days { get; set; }

        [Required, Range(1, 10)]
        public int Sets { get; set; }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
