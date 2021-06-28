using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Responses.Exercise
{
    public class SelectExerciseModel
    {
        public int ExerciseId { get; set; } = 0;

        public string ExerciseName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;
    }
}
