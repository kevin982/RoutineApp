using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Data.Entities
{
    public class ExerciseCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
