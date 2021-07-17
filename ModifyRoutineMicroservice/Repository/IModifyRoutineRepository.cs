using DomainRoutineLibrary.Entities;
using System.Threading.Tasks;

namespace ModifyRoutineMicroservice.Repository
{
    public interface IModifyRoutineRepository
    {
        Task AddDayToExercise(int exerciseId, int dayId);
        Task<Exercise> GetExerciseByIdAsync(int exerciseId, string userId);
        Task UpdateExerciseAsync(Exercise ex);

        Task<Day> GetDayByIdAsync(int dayId);
    }
}