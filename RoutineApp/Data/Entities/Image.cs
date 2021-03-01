using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Data.Entities
{
    public class Image
    {
        public int Id { get; set; } = 0;

        public string Url { get; set; } = string.Empty;


        public int ExerciseId { get; set; } = 0;

        public Exercise Excercise { get; set; } = new();
    }
}
