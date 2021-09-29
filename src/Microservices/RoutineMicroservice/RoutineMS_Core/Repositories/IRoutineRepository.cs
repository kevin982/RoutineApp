using RoutineMS_Core.Dtos;
using RoutineMS_Core.Models.Entities;
using System;
using System.Threading.Tasks;

namespace RoutineMS_Core.Repositories
{
    public interface IRoutineRepository : IRepository<Routine>
    {
        Task<ExerciseToDoDto> GetExerciseToDoFromRoutineAsync(Guid userId, int day);
        Task<bool> RemoveRoutineByExerciseAsync(Guid exerciseId, Guid userId);
        Task<bool> TheExerciseIsInRoutineAsync(Guid exerciseId, Guid userId);

    }
}
