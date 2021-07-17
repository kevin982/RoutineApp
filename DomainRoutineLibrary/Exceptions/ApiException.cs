using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineLibrary.Exceptions
{
    public class ApiException : Exception
    {
        public string Error { get; set; } = string.Empty; 

        public int HttpCode { get; set; } = 0;

        public string Microservice { get; set; } = string.Empty;

        public string Method { get; set; } = string.Empty;

        public string Class { get; set; } = string.Empty;

        public override string ToString()
        {
            return ($"The microservice {Microservice} has throw an error in the method {Method} which belongs to the class {Class} because of {Error}");
        }

    }
}
