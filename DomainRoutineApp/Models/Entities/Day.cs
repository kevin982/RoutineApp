using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Entities
{
    public class Day
    {
        public int Id { get; set; } = 0;

        public string DayName { get; set; } = string.Empty;

        public List<Exercise> Exercises { get; set; } = new();
    }
}
