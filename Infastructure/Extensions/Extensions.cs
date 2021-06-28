using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Extensions
{
    public static class Extensions
    {
        public static string GetKevinDate(this DateTime date)
        {
            string number = "", month = "", year = "";

            int day = date.Day;

            switch (date.Month)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;

                case 3:
                    month = "March";
                    break;

                case 4:
                    month = "April";
                    break;

                case 5:
                    month = "May";
                    break;

                case 6:
                    month = "June";
                    break;

                case 7:
                    month = "July";
                    break;

                case 8:
                    month = "August";
                    break;

                case 9:
                    month = "September";
                    break;

                case 10:
                    month = "October";
                    break;

                case 11:
                    month = "November";
                    break;

                default:
                    month = "December";
                    break;
            }

            if (day == 1 || day == 21 || day == 31)
            {
                number = day + "st";
            }
            else if (day == 2 || day == 22)
            {
                number = day + "nd";
            }
            else if (day == 3 || day == 23)
            {
                number = day + "rd";
            }
            else
            {
                number = day + "th";
            }

            year = date.Year.ToString();

            return $"{month} {number} {year}";
        }
    }
}
