using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Entities
{
    public class ExerciseSetDetail
    {
        public int Id { get; set; } = 0;

        public int ExerciseId { get; set; } = 0;

        public Exercise Exercise { get; set; } = new();

        public DateTime DayDone { get; set; } = new();

        public int Weight { get; set; } = 0;

        public int Repetitions { get; set; } = 0;

        public int SetNumber { get; set; } = 0;
    }
}
