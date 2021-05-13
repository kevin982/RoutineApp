using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Responses.Statics
{
    public class WeightStatisticsResponseModel
    {
        public int LowestWeight { get; set; } = 0;
        public int HighestWeight { get; set; } = 0;
        public int CurrentWeight { get; set; } = 0;
        public int FirstWeight { get; set; } = 0;
    }
}
