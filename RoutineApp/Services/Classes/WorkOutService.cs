using RoutineApp.Data;
using RoutineApp.Data.Entities;
using RoutineApp.Mappers.Classes;
using RoutineApp.Mappers.Interfaces;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class WorkOutService: IWorkOutService
    {
        private readonly RoutineContext _context = null;
        private readonly IExerciseService _exerciseService = null;
        private readonly IUserService _userService = null;
        private readonly IExerciseMapper _exerciseMapper = null;

        public WorkOutService(RoutineContext context, IExerciseService exerciseService, IUserService userService, IExerciseMapper exerciseMapper)
        {
            _context = context;
            _exerciseService = exerciseService;
            _userService = userService;
            _exerciseMapper = exerciseMapper;
        }

        public async Task<ExerciseWorkOutResponseModel> GetNextExerciseAsync()
        {

            Stopwatch watch = new();

            watch.Start();

            var todayExercises = await _exerciseService.GetExercisesForTodayAsync(_userService.GetUserId());

            watch.Stop();

            var mili = watch.ElapsedMilliseconds;



            if (todayExercises.Count == 0 || todayExercises == null) return new ExerciseWorkOutResponseModel { Status = "You do not have exercises for today" };

            foreach (var exercise in todayExercises)
            {
                int setsDone = await _exerciseService.GetExerciseSetsDoneToday(exercise.Id);

                if (setsDone < exercise.Sets)
                {
                    var exerciseResponse = _exerciseMapper.MapDomainToExerciseWorkOutResponse(exercise);
                    exerciseResponse.Status = "Ok";
                    exerciseResponse.RepetitionsLeft = exercise.Sets - setsDone;
                    return exerciseResponse;
                }
            }

            return new ExerciseWorkOutResponseModel { Status = "You have already worked out for today!" };
        }




    }
}
