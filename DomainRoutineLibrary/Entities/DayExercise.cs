using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineLibrary.Entities
{
    public class DayExercise
    {
        public int Id { get; set; } = 0;

        public int DayId { get; set; } = 0;

        public Day Day { get; set; } = new Day();
        
        public int ExerciseId { get; set; } = 0;

        public Exercise Exercise { get; set; } = new();

    }
}
