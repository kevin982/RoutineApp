using System;
using System.ComponentModel.DataAnnotations;

namespace RoutineMS_Core.Models.Requests
{
    public class SetDoneRequest
    {
        [Required]
        public Guid ExerciseId { get; set; }

        [Required]
        public int PoundsLifted { get; set; }

        [Required]
        public int Repetitions { get; set; }

        public DateTime DateDone { get; set; }
    }
}
