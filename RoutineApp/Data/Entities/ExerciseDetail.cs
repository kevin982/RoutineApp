using System;

namespace RoutineApp.Data.Entities
{
    public class ExerciseDetail
    {
        public int Id { get; set; } = 0;

        public int ExerciseId { get; set; } = 0;

        public Exercise Exercise { get; set; } = new();

        public DateTime DayDone { get; set; } = new();

        public float Weight { get; set; } = 0;

        public int Repetitions { get; set; } = 0;

    }
}