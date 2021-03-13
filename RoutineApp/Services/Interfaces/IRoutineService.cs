using RoutineApp.Data.Entities;
using RoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Interfaces
{
    public interface IRoutineService
    {
        Task<bool> CreateExerciseAsync(CreateExerciseModel model);

        Task<List<ExerciseCategory>> GetCategoriesAsync();

        Task<List<Exercise>> GetExercisesAsync();
    }

}
