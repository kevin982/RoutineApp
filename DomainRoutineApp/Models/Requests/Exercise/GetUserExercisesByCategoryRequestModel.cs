using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Exercise
{
    public class GetUserExercisesByCategoryRequestModel
    {
        public string UserId { get; set; } = string.Empty;

        public int CategoryId { get; set; } = 0;    
    }
}
