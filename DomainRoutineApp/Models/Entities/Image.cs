using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Entities
{
    public class Image
    {
        public int Id { get; set; } = 0;

        public string Img { get; set; } = string.Empty;

        public int ExerciseId { get; set; } = 0;

        public Exercise Excercise { get; set; } = new();
    }
}
