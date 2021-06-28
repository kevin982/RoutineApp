using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Responses.Statics
{
    public class ExerciseStatisticsResponseModel
    {
        public int FirstWeight { get; set; } = 0;
        
        public int CurrentWeight { get; set; } = 0;
        
        public int LowestWeight{ get; set; } = 0;
        
        public int HighestWeight { get; set; } = 0;

        public List<string> Images = new();

        public int ExerciseId { get; set; } = 0;

        public string ExerciseName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public double RepetitionsAverage { get; set; } = 0;

        public string FirstDay { get; set; } = string.Empty;

        public string LastDay { get; set; } = string.Empty;
    }
}
