using DomainRoutineLibrary.Entities;
using DomainRoutineLibrary.Exceptions;
using DomainRoutineLibrary.Services;
using ModifyRoutineMicroservice.Models;
using ModifyRoutineMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModifyRoutineMicroservice.Services
{
    public class ModifyRoutineService : IModifyRoutineService
    {
        private readonly IModifyRoutineRepository _exerciseRepository = null;
        private readonly IUserService _userService = null;

        public ModifyRoutineService(IModifyRoutineRepository exerciseRepository, IUserService userService)
        {
            _exerciseRepository = exerciseRepository;
            _userService = userService;
        }

        public async Task AddExerciseToRoutineAsync(AddExerciseToRoutineRequestModel model)
        {
            try
            {
                //TODO: I HAVE TO CHANGE THE DEFAULT USER ID FOR THE ACTUAL ID.


                //string userId = _userService.GetUserId();
                string userId = "2789681b-621d-492a-a92d-940aa2ffd8dc";

                if (string.IsNullOrEmpty(userId)) throw new ApiException 
                { 
                    Error = "The user is not authenticated becasuse his id has not been found.", 
                    HttpCode =  403, 
                    Class = nameof(ModifyRoutineService), 
                    Method = nameof(AddExerciseToRoutineAsync), 
                    Microservice = nameof(ModifyRoutineMicroservice)
                };

                var exercise = await _exerciseRepository.GetExerciseByIdAsync(model.ExerciseId, userId);

                exercise.IsInTheRoutine = true;

                exercise.Sets = model.Sets;

                foreach (int day in model.Days) 
                {
                    var d = await _exerciseRepository.GetDayByIdAsync(day);

                    if (d is not null) exercise.DaysToTrain.Add(d);
                }

                await _exerciseRepository.UpdateExerciseAsync(exercise);

            }
            catch (ApiException ex)
            {
                throw ex;
            }

            
        }

        public async Task RemoveExerciseFromRoutineAsync(int exerciseId)
        {

            try
            {
                //TODO: I HAVE TO CHANGE THE DEFAULT USER ID FOR THE ACTUAL ID.

                //string userId = _userService.GetUserId();

                string userId = "2789681b-621d-492a-a92d-940aa2ffd8dc";

                if (string.IsNullOrEmpty(userId)) throw new Exception("The user is not authenticated because his id has not been found.");

                Exercise exercise;
                
                exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId, userId);
                exercise.Sets = 0;
                exercise.IsInTheRoutine = false;
                //await _exerciseRepository.DeleteDaysToTrainAsync(new DeleteDayToTrainRequestModel { ExerciseId = exercise.Id });
                exercise.DaysToTrain.Clear();

                await _exerciseRepository.UpdateExerciseAsync(exercise);
            }
            catch (ApiException ex)
            {
                throw ex;
            }
 
        }


    }
}
