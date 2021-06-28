using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Responses.Exercise
{
    public class CreateRoutineExerciseResponseModel
    {
        public List<string> Images = new();
        public int ExerciseId { get; set; } = 0;
        public string ExerciseName { get; set; } = string.Empty;
        public bool IsInTheRoutine { get; set; } = false;
        public string Category { get; set; } = string.Empty;

    }
}
