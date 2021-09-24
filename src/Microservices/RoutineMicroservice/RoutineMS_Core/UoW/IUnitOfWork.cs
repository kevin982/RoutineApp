using RoutineMS_Core.Repositories;
using System.Threading.Tasks;

namespace RoutineMS_Core.UoW
{
    public interface IUnitOfWork
    {
        IExerciseRepository Exercises { get; }
        IRoutineRepository Routines { get; }
        IDayRepository Days { get; }
        ISetDetailRepository SetsDetails { get; }
        Task CompleteAsync();
    }
}
