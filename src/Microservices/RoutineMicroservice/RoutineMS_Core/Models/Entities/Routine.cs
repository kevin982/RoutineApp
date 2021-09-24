using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Core.Models.Entities
{
    public class Routine
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public int Sets { get; set; }

        public IEnumerable<Day> Days { get; set; }
    }
}
