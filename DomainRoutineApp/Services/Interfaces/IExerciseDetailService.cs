using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.ExerciseDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IExerciseDetailService
    {
        Task<int> GetExerciseSetsDoneAsync(GetExerciseSetsDoneTodayRequestModel model);

        Task<int> CreateExerciseDetailAsync(ExerciseDoneRequestModel model);

        Task<ExerciseDetail> GetExerciseDetailByIdAsync(GetExerciseDetailByIdRequestModel model);
    }
}
