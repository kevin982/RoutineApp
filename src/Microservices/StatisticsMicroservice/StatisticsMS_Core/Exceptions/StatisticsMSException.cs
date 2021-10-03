using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Core.Exceptions
{
    public class StatisticsMSException : Exception
    {
        public int StatusCode { get; set; }

        public StatisticsMSException(string message) : base(message) { }

}
}
