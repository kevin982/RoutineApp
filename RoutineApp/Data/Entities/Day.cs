using System.Collections.Generic;

namespace RoutineApp.Data.Entities
{
    public class Day
    {
        public int Id { get; set; } = 0;

        public string DayName { get; set; } = string.Empty;

        public List<Exercise> Exercises { get; set; } = new();
    }
}