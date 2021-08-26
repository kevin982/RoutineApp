using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Models.Responses
{
    public class HateoasExerciseMSResponse<T>
    {
        public IEnumerable<Link> Links { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public T Content { get; set; }
        
    }

}
