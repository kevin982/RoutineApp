using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Models.Requests.Statics
{
    public class GetExerciseStatisticsRequestModel
    {
        [Display(Name = "Please select the exercise")]
        public int ExerciseId { get; set; } = 0;


    }
}
