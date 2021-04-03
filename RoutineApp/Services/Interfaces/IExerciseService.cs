using RoutineApp.Data.Entities;
using RoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<bool> CreateExerciseAsync(CreateExerciseModel model);

        Task<List<ExerciseCategory>> GetCategoriesAsync();

        Task<List<Exercise>> GetExercisesAsync();

        Task<List<Exercise>> GetAllExercisesAsync();

        Task AddExerciseToRoutineAsync(AddExerciseModel model);

        Task RemoveExerciseFromRoutineAsync(int id);
    }

}
