using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Dtos
{
    public class DtoExercise
    {
        public Guid Id { get; set; }

        public string ExerciseName { get; set; }

        public string CategoryName { get; set; }

        public bool IsInTheRoutine { get; set; }

        public string ImageUrl { get; set; }
    }
}
