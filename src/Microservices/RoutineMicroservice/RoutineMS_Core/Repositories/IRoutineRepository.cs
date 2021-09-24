using RoutineMS_Core.Dtos;
using RoutineMS_Core.Models.Entities;
using System;
using System.Threading.Tasks;

namespace RoutineMS_Core.Repositories
{
    public interface IRoutineRepository : IRepository<Routine>
    {
        Task<ExerciseToDoDto> GetExerciseToDoFromRoutineAsync(Guid userId);

        Task<bool> RemoveRoutineByExercise(Guid exerciseId);
    }
}
