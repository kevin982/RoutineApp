using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Exercise
{
    public class AddExerciseToRoutineRequestModel
    {
        public int ExerciseId { get; set; } = 0;

        public List<int> Days { get; set; } = new();

        [Required(ErrorMessage = "You must enter the set number"), Range(2, 5)]
        public int Sets { get; set; } = 0;
    }
}
