using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Core.Models.Entities
{
    public class SetDetail
    {
        public Guid Id { get; set; }

        public Exercise Exercise { get; set; }
        public Guid ExerciseId { get; set; }
        public DateTime Date { get; set; }
        public int SetsCompleted { get; set; }
    }
}
