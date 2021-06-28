using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCoreApp.Models
{
    public class ExerciseCarouselModel
    {
        public List<string> Images { get; set; } = new();

        public int ExerciseId { get; set; } = 0;

        public string ExerciseName { get; set; } = string.Empty;

        public string ExerciseCategory { get; set; } = string.Empty;
    }
}
