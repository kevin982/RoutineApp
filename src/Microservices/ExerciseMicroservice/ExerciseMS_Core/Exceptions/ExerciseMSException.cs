﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Exceptions
{
    public class ExerciseMSException : Exception
    {
        public int StatusCode { get; set; }
 
        public ExerciseMSException(string message):base(message){}

    }
}
