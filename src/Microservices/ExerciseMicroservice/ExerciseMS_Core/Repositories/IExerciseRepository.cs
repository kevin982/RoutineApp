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
        Task<IEnumerable<Exercise>> GetAllExercisesByCategoryAsync(Guid categoryId, Guid userId,int index, int size);

        int GetExerciseCountByCategory(Guid categoryId, Guid userId);
    }
}
