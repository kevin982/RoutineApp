using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Entities
{
    public class Exercise
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; } = 0;

        public ExerciseCategory Category { get; set; } = new();

        public string Image { get; set; } = string.Empty;

        public List<ExerciseSetDetail> ExerciseSetDetails { get; set; } = new();

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = new();

        public List<Day> DaysToTrain { get; set; } = new();

        public bool IsInTheRoutine { get; set; } = false;

        public int Sets { get; set; } = 0;
    }
}
