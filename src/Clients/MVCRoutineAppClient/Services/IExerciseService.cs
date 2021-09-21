using MVCRoutineAppClient.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface IExerciseService
    {

        Task<JObject> CreateExerciseAsync(CreateExerciseRequestModel model, string accessToken);

        Task<string> GetExercisesByCategory(string accessToken, Guid categoryId, int index, int size);

        Task<string> GetIndexesCount(string accessToken, Guid categoryId, int size);
    }
}