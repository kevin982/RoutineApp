using ExerciseMS_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Repositories
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        Task<IEnumerable<Exercise>> GetAllExercisesByCategoryAsync(Guid categoryId,int index, int size);

        int GetIndexesCount(Guid categoryId, int size);

        Task<bool> UpdateIsInTheRoutineAsync(bool newValue, Guid exerciseId, Guid userId);
        Task<IEnumerable<Exercise>> GetExercisesNameAndIdByCategoryAsync(Guid userId, Guid categoryId);
    }
}
