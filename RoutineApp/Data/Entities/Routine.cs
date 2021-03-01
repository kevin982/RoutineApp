using System.Collections.Generic;

namespace RoutineApp.Data.Entities
{
    public class Routine
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public List<Exercise> Exercises { get; set; } = new();

        public List<User> Users { get; set; } = new();

    }
}