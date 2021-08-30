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

        int GetExerciseCountByCategory(Guid categoryId);

        Task<bool> UpdateIsInTheRoutine(bool newValue, Guid exerciseId, Guid userId);
    }
}
