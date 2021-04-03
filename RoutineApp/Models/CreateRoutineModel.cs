using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Models
{
    public class CreateRoutineModel
    {
        public string Name { get; set; } = string.Empty;

        public List<int> Exercises { get; set; } = new();

        

    }
}
