﻿using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.ExerciseCategory;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services
{
    public class ExerciseCategoryService : IExerciseCategoryService
    {
        private readonly IExerciseCategoryRepository _exerciseCategoryRepository = null;

        public ExerciseCategoryService(IExerciseCategoryRepository exerciseCategoryRepository)
        {
            _exerciseCategoryRepository = exerciseCategoryRepository;
        }
        
        public async Task<List<ExerciseCategory>> GetAllCategoriesAsync()
        {
            return await _exerciseCategoryRepository.GetAllCategoriesAsync();
        }

        public async Task<ExerciseCategory> GetExerciseByIdAsync(GetExerciseCategoryByIdRequestModel model)
        {
            return await _exerciseCategoryRepository.GetCategoryByIdAsync(model);
        }
    }
}
