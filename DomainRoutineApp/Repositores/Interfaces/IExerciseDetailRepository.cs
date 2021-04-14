using DomainRoutineApp.Models.Requests.ExerciseDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Repositores.Interfaces
{
    public interface IExerciseDetailRepository
    {
        Task<int> GetExerciseSetsDoneTodayAsync(GetExerciseSetsDoneTodayRequestModel model);
    }
}
