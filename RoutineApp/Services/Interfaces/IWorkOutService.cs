using RoutineApp.Models;
using System.Threading.Tasks;

namespace RoutineApp.Services.Interfaces
{
    public interface IWorkOutService
    {
        Task<ExerciseWorkOutResponseModel> GetNextExerciseAsync();
    }
}
