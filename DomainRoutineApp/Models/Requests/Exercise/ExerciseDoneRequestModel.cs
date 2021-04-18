using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Exercise
{
    public class ExerciseDoneRequestModel
    {
        [Required]
        public float Weight { get; set; } = 0;

        [Required, Range(1,500)]
        public int Repetitions { get; set; } = 0;

        public int ExerciseId { get; set; } = 0;
    }
}
