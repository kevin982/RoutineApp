using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Core.Models.Entities
{
    public class Statistic
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Weight { get; set; }
        public int Repetitions { get; set; }
        public DateTime DayDone { get; set; }
    }
}
