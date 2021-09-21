using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Extensions
{
    public static class IntExtensions
    {
        public static decimal ToDecimal(this int number)
        {
            string numberString = $"{number}.0";

            return Convert.ToDecimal(numberString);
        }
    }
}
