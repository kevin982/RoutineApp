using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Models.Entities
{
    public class Exercise
    {
        public Guid ExerciseId { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public string ExerciseName { get; set; }

        public bool IsInTheRoutine { get; set; }

        public string ImageUrl { get; set; }
    }
}
