using MVCRoutineAppClient.Models;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface IExerciseService
    {
        Task<(bool, dynamic)> GetAllCategoriesAsync(string accessToken);

        Task<(bool, string)> CreateCategoryAsync(CreateCategoryRequestModel model, string accessToken);

        Task<(bool, string)> CreateExerciseAsync(CreateExerciseRequestModel model, string accessToken);
    }
}