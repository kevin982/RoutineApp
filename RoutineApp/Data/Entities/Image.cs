using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public byte[] Img { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Excercise { get; set; }
    }
}
