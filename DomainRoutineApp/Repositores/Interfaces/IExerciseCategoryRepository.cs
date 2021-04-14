﻿using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.ExerciseCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Repositores.Interfaces
{
    public interface IExerciseCategoryRepository
    {
        Task<List<ExerciseCategory>> GetAllCategoriesAsync();

        Task<ExerciseCategory> GetCategoryByIdAsync(GetExerciseCategoryByIdRequestModel model);
    }
}
