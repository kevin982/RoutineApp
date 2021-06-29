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
    public interface IExerciseSetDetailsService
    {
        Task<int> GetExerciseSetsDoneAsync(GetExerciseSetsDoneTodayRequestModel model);

        Task<int> CreateExerciseSetDetailAsync(ExerciseDoneRequestModel model);

        Task<ExerciseSetDetail> GetExerciseSetDetailsByIdAsync(GetExerciseDetailByIdRequestModel model);
    }
}
