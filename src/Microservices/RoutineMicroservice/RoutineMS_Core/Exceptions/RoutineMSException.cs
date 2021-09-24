using System;

namespace RoutineMS_Core.Exceptions
{
    public class RoutineMSException : Exception
    {
        public int StatusCode { get; set; }

        public RoutineMSException(string message) : base(message) { }
    }
}
