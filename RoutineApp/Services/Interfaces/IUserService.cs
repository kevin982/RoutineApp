using RoutineApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public interface IUserService
    {
        Task<List<Exercise>> GetExercisesAsync();
        string GetUserId();
    }
}