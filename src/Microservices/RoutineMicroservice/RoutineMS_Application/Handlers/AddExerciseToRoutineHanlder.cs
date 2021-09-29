using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RoutineMS_Application.Commands;
using RoutineMS_Application.Validator;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Core.Models.Requests;
using RoutineMS_Core.Services;
using RoutineMS_Core.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutineMS_Application.Handlers
{
    public class AddExerciseToRoutineHanlder : IRequestHandler<AddExerciseToRoutineCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddExerciseToRoutineCommand> _validator;
        private readonly IUserService _userService;
        private readonly IPublisherService _publisherService;
        private readonly IConfiguration Configuration;

        private Exercise Exercise { get; set; }
        private Routine Routine{ get; set; }
        private Guid UserId {  get; set; }

        public AddExerciseToRoutineHanlder(IUnitOfWork unitOfWork, IValidator<AddExerciseToRoutineCommand> validator, IUserService userService, IPublisherService publisherService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _userService = userService;
            _publisherService = publisherService;
            Configuration = configuration;
        }

        public async Task<bool> Handle(AddExerciseToRoutineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                UserId = new Guid(_userService.GetUserId());

                await _validator.ValidateAndThrowAsync(request);
                
                var routine = await _unitOfWork.Routines.TheExerciseIsInRoutineAsync(request.Request.ExerciseId, UserId);
                
                if (routine) throw new RoutineMSException("The exercise is already in the routine!") { StatusCode = 400};
                
                await Map(request.Request);

                await _unitOfWork.Exercises.CreateAsync(Exercise);
                await _unitOfWork.Routines.CreateAsync(Routine);
                await _unitOfWork.CompleteAsync();
                _publisherService.PublishEvent(new { UserId = Routine.UserId, ExerciseId = Exercise.Id, NewValue = true}, Configuration["Events:UpdateExercise:Exchange"], Configuration["Events:UpdateExercise:RoutingKey"]);

                return true;
            }
            catch (Exception)
            {
                throw;
            }


        }

        private async Task Map(AddExerciseToRoutineRequest request)
        {
            try
            {
                List<Day> days = new();

                foreach (var day in request.Days) days.Add(await _unitOfWork.Days.GetDayByDayNumberAsync(day));

                Exercise = new Exercise()
                {
                    Id = request.ExerciseId,
                    Name = request.ExerciseName,
                    ImageUrl = request.ImageUrl,
                    CategoryName = request.CategoryName
                };

                Routine = new Routine()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Exercise = Exercise,
                    ExerciseId = request.ExerciseId,
                    Days = days,
                    Sets = request.Sets
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
