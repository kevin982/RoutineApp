using MVCRoutineAppClient.Models;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface IExerciseService
    {

        Task<(bool, string)> CreateExerciseAsync(CreateExerciseRequestModel model, string accessToken);
    }
}