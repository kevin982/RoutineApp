using ModifyRoutineMicroservice.Models;
using System.Threading.Tasks;

namespace ModifyRoutineMicroservice.Services
{
    public interface IModifyRoutineService
    {
        Task AddExerciseToRoutineAsync(AddExerciseToRoutineRequestModel model);
        Task RemoveExerciseFromRoutineAsync(int exerciseId);
    }
}