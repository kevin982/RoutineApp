using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineLibrary.Models
{
    public class ApiResponseModel
    {
        public List<LinkModel> Links { get; set; } = new();

        public object Data { get; set; } = null;

        public string Message { get; set; } = string.Empty;

        

    }
}
