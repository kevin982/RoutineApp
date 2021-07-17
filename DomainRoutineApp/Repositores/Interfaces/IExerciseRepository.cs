
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Repositores.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetAllUserExercisesAsync(GetAllExercisesRequestModel model);

        Task CreateExerciseAsync(Exercise model);

        Task<List<Exercise>> GetTodayExercisesAsync(GetTodayExercisesRequestModel model);

        Task<Exercise> GetExerciseByIdAsync(GetExerciseRequestModel model);

        Task UpdateExerciseAsync(Exercise exercise);

        Task DeleteDaysToTrainAsync(DeleteDayToTrainRequestModel model);

        Task<List<Exercise>> GetUserExerciseByCategoryAsync(GetUserExercisesByCategoryRequestModel model);
 

    }
}
