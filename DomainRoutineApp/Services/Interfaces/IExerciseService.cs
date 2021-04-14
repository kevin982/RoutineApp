using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Responses.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IExerciseService
    {
        Task CreateExerciseAsync(CreateExerciseRequestModel model);
 
        Task AddExerciseToRoutineAsync(AddExerciseToRoutineRequestModel model);

        Task RemoveExerciseFromRoutineAsync(RemoveExerciseFromRoutineRequestModel model);

        Task<List<Exercise>> GetTodayExercisesAsync(GetTodayExercisesRequestModel model);

        Task<List<CreateRoutineExerciseResponseModel>> GetAllUserExercises();
    }
}
