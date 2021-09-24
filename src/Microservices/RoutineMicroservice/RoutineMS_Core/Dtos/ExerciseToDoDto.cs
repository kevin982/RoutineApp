using System;

namespace RoutineMS_Core.Dtos
{
    public class ExerciseToDoDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        
        public int SetsLeft { get; set; }

    }
}
