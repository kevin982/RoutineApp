using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class ExerciseWorkOutResponseModel
    {
        public int ExerciseId { get; set; } = 0;
        public int RepetitionsLeft { get; set; } = 0;

        public string ExerciseName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public List<string> Images { get; set; } = new();
    }
}
