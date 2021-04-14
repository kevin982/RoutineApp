using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Exercise
{
    public class ExerciseDoneRequestModel
    {
        public int Weight { get; set; } = 0;

        public int Repetitions { get; set; } = 0;

        public int ExerciseId { get; set; } = 0;
    }
}
