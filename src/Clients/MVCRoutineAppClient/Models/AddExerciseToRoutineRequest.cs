using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Models
{
    public class AddExerciseToRoutineRequest
    {
        [Required]
        public Guid ExerciseId { get; set; }

        [Required, MinLength(1)]
        public IEnumerable<int> Days { get; set; }

        [Required, Range(1,10)]
        public int Sets { get; set; }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsValid()
        {
            if (ExerciseId == Guid.Empty) return false;

            if (Days.Count() == 0) return false;

            if (Sets <= 0) return false;

            return true;
        }
    }
}
