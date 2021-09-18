using MVCRoutineAppClient.Models;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface ICategoryService
    {
        Task<(bool, string)> CreateCategoryAsync(CreateCategoryRequestModel model, string accessToken);
        Task<(bool, dynamic)> GetAllCategoriesAsync(string accessToken);
    }
}