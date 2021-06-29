using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.ExerciseDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Repositores.Interfaces
{
    public interface IExerciseSetDetailsRepository
    {
        Task<int> GetExerciseSetsDoneTodayAsync(GetExerciseSetsDoneTodayRequestModel model);

        Task<int> CreateExerciseSetDetailAsync(ExerciseSetDetail exerciseDetail);

        Task<ExerciseSetDetail> GetExerciseSetDetailsByIdAsync(GetExerciseDetailByIdRequestModel model);
    }
}
