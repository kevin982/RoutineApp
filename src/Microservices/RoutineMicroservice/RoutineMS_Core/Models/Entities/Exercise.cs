using System;
using System.Collections.Generic;

namespace RoutineMS_Core.Models.Entities
{
    public class Exercise
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public SetDetail SetDetail { get; set; }
    }
}
