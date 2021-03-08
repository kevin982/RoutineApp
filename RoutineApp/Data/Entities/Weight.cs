using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Data.Entities
{
    public class Weight
    {
        public int Id { get; set; }

        public float ExactWeight { get; set; } = 0;

        public DateTime RegisteredOn { get; set; } = new();

        public int UserId { get; set; }

        public User User { get; set; } = new();
    }
}
