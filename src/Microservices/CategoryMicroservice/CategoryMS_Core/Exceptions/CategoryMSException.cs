using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Core.Exceptions
{
    public class CategoryMSException : Exception
    {
        public int StatusCode { get; set; }

        public CategoryMSException(string message) : base(message) { }
    }
}
