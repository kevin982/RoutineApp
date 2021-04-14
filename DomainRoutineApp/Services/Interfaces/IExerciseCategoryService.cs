using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.ExerciseCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IExerciseCategoryService
    {
        Task<List<ExerciseCategory>> GetAllCategoriesAsync();

        Task<ExerciseCategory> GetExerciseByIdAsync(GetExerciseCategoryByIdRequestModel model);
    }
}
