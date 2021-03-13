namespace RoutineApp.Data.Entities
{
    public class ExerciseDetail
    {
        public int Id { get; set; } = 0;

        public int ExerciseId { get; set; } = 0;

        public Exercise Exercise { get; set; } = new();

        public int Day { get; set; }

        public float Weight { get; set; } = 0;

        public int Repetitions { get; set; } = 0;

        public int Sets { get; set; } = 0;
    }
}