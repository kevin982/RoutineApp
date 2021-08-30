using ExerciseMS_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.UoW
{
    public interface IUnitOfWork
    {
        IExerciseRepository Exercises { get; }
        ICategoryRepository Categories { get; }
        Task CompleteAsync();
    }
}
