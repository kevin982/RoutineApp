using Microsoft.EntityFrameworkCore;
using RoutineApp.Data;
using RoutineApp.Data.Entities;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class RoutineService : IRoutineService
    {
        private readonly RoutineContext _context = null;
        private readonly IUserService _userService = null;

        public RoutineService(RoutineContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<bool> CreateExerciseAsync(CreateExerciseModel model)
        {
            try
            {
                Exercise exercise = (Exercise)model.Clone();

                exercise.UserId = _userService.GetUserId();
                
                await _context.Exercises.AddAsync(exercise);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
 
        }

        public async Task<List<ExerciseCategory>> GetCategoriesAsync()
        {
            return await _context.ExerciseCategories.AsNoTracking().ToListAsync();
        } 
    }
}
