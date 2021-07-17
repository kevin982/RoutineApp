using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineLibrary.Entities
{
    public class ExerciseCategory
    {
        public int Id { get; set; } = 0;

        public string CategoryName { get; set; } = string.Empty;

        public List<Exercise> Exercises { get; set; } = new();
    }
}
