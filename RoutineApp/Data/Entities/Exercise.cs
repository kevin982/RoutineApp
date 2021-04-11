using System.Collections.Generic;

namespace RoutineApp.Data.Entities
{
    public class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public ExerciseCategory Category { get; set; }

        public List<Image> Images { get; set; }

        public List<ExerciseDetail> ExerciseDetails { get; set; }
 
        public string UserId { get; set; }

        public User User { get; set; }

        public List<Day> DaysToTrain { get; set; }

        public bool IsInTheRoutine { get; set; }

        public int Sets { get; set; }
    }
}