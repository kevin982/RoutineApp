using System;
using System.Collections.Generic;

namespace RoutineApp.Data.Entities
{
    public class Routine
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = new();

        public List<Exercise> Exercises { get; set; } = new();

        public DateTime CreatedOn { get; set; }


    }

   
}