using MVCRoutineAppClient.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface ICategoryService
    {
        Task<JObject> CreateCategoryAsync(CreateCategoryRequestModel model, string accessToken);
        Task<string> GetAllCategoriesAsync(string accessToken);
    }
}