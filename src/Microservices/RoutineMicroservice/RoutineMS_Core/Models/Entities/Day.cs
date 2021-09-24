using System.Collections.Generic;

namespace RoutineMS_Core.Models.Entities
{
    public class Day
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Routine> Routines { get; set; }
    }
}
