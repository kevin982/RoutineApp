using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.ExerciseDetail;
using DomainRoutineApp.Models.Responses.Exercise;
using DomainRoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services.Classes
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IExerciseService _exerciseService = null;
        private readonly IUserService _userService = null;
        private readonly IExerciseDetailService _exerciseDetailService = null;

        public WorkoutService(IExerciseService exerciseService, IUserService userService, IExerciseDetailService exerciseDetailService)
        {
            _exerciseService = exerciseService;
            _userService = userService;
            _exerciseDetailService = exerciseDetailService;
        }

        public async Task CreateAndAddExerciseDetailAsync(ExerciseDoneRequestModel model)
        {
            int exerciseDetailId = await _exerciseDetailService.CreateExerciseDetailAsync(model);
        }

        public async Task<ExerciseWorkOutResponseModel> GetNextExerciseAsync()
        {

            var todayExercises = await _exerciseService.GetTodayExercisesAsync(new GetTodayExercisesRequestModel { UserId = _userService.GetUserId() });

            if (todayExercises.Count == 0) return new ExerciseWorkOutResponseModel { Status = "You do not have exercise to do today." };

            int setsDone = 0;

            foreach (var exercise in todayExercises)
            {
                setsDone = await _exerciseDetailService.GetExerciseSetsDoneAsync(new GetExerciseSetsDoneTodayRequestModel() { ExerciseId = exercise.Id });

                if (setsDone < exercise.Sets)
                {
                    return new ExerciseWorkOutResponseModel
                    {
                        ExerciseId = exercise.Id,
                        Images = exercise.Images,
                        ExerciseName = exercise.Name,
                        RepetitionsLeft = exercise.Sets - setsDone,
                        Status = "Ok"
                    };
                }

            }

            return new ExerciseWorkOutResponseModel { Status = "You have already did your exericises for today." };

        }
    }
}
