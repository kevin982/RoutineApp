using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class AddExerciseModel
    {
        public int ExerciseId { get; set; } = 0;

        public List<int> Days { get; set; } = new();
    }
}
