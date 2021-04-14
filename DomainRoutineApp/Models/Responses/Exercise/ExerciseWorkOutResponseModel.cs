using DomainRoutineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Responses.Exercise
{
    public class ExerciseWorkOutResponseModel
    {
        public int ExerciseId { get; set; } = 0;

        public int RepetitionsLeft { get; set; } = 0;

        public string ExerciseName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public List<Image> Images { get; set; } = new();
    }
}
