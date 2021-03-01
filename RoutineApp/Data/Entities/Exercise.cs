using System.Collections.Generic;

namespace RoutineApp.Data.Entities
{
    public class Exercise
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public List<Image> Images { get; set; } = new();

        public List<ExerciseDetail> ExerciseDetails { get; set; }
    }
}