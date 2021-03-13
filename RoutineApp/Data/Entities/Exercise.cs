﻿using System.Collections.Generic;

namespace RoutineApp.Data.Entities
{
    public class Exercise
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public ExerciseCategory Category { get; set; }

        public List<Image> Images { get; set; } = new();

        public List<ExerciseDetail> ExerciseDetails { get; set; }

        public List<Routine> Routines { get; set; }

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = new User();
    }
}