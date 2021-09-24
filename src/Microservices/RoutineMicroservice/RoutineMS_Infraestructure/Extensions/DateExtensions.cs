using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Extensions
{
    public static class DateExtensions
    {
        public static int GetDayOfWeek(this DateTime date)
        {
            string day = date.DayOfWeek.ToString().ToLower();

            switch (day)
            {
                case "monday": return 1; 
                
                case "tuesday":return 2;
                
                case "wednesday":return 3;
                
                case "thursday":return 4;
                
                case "friday": return 5;

                case "saturday": return 6;

                default: return 7;
            }
        }
    }
}
