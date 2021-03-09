using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RoutineApp.Data;
using RoutineApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly RoutineContext _context = null;
        public UserService(IHttpContextAccessor httpContext, RoutineContext context)
        {
            _httpContext = httpContext;
            _context = context;
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<List<Routine>> GetRoutinesAsync()
        {
            string id = GetUserId();

            return await _context.Routines.AsNoTracking().Include(routine => routine.Exercises).Where(routine => routine.UserId == id).ToListAsync();
        }
        public async Task<List<Exercise>> GetExercisesAsync()
        {
            string id = GetUserId();

            return await _context.Exercises.AsNoTracking().Include(exercice => exercice.ExerciseDetails).Where(exercise => exercise.UserId == id).ToListAsync();
        }


    }
}
