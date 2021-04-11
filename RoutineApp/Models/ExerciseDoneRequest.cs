using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class ExerciseDoneRequest
    {
        public int ExerciseId { get; set; } = 0;

        [Required(ErrorMessage = "You must enter the weight you lifted"), Display(Name = "Please enter the weight that you lifted")]
        public float Weight { get; set; } = 0;

        [Required(ErrorMessage = "You must enter the repetions that you have done."), Display(Name = "Please enter the repetions that you have done")]
        public int Repetitions { get; set; } = 0;
    }
}
