using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Models.Requests
{
    public class UpdateExerciseRequest
    {
        public Guid UserId { get; set; }

        public Guid ExerciseId { get; set; }

        public bool NewValue { get; set; }
    }
}
