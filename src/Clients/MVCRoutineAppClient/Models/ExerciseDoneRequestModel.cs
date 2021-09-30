using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Models
{
    public class ExerciseDoneRequestModel
    {
        [Required]
        public Guid ExerciseId { get; set; }

        [Required]
        public int PoundsLifted { get; set; }

        [Required]
        public int Repetitions { get; set; }
 
    }
}
