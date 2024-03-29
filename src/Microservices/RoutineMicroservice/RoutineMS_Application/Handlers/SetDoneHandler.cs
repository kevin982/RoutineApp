﻿using MediatR;
using Microsoft.Extensions.Configuration;
using RoutineMS_Application.Commands;
using RoutineMS_Core.Models.Entities;
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
    public class SetDoneHandler : IRequestHandler<SetDoneCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisherService _publisherService;
        private readonly IConfiguration Configuration;
        private readonly IUserService _userService;

        public SetDoneHandler(IUnitOfWork unitOfWork, IPublisherService publisherService, IConfiguration configuration, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _publisherService = publisherService;
            Configuration = configuration;
            _userService = userService;
        }

        public async Task<bool> Handle(SetDoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = new(_userService.GetUserId());

                Exercise exercise = await _unitOfWork.Exercises.GetByIdAsync(request.Request.ExerciseId);

                await _unitOfWork.SetsDetails.DeleteOldDetailsAsync();
                await _unitOfWork.CompleteAsync();

                if (exercise.SetDetail is not null)
                {
                    exercise.SetDetail.SetsCompleted++;
                }
                else
                {
                    SetDetail setDetail = new() { Id = Guid.NewGuid(), SetsCompleted = 1, Date = DateTime.UtcNow };

                    await _unitOfWork.SetsDetails.CreateAsync(setDetail);

                    exercise.SetDetail = setDetail;
                }
                await _unitOfWork.CompleteAsync();    

                var setDone = new {UserId = userId, Id = Guid.NewGuid(),ExerciseId = request.Request.ExerciseId, Weight = request.Request.PoundsLifted, Repetitions = request.Request.Repetitions, DayDone = DateTime.UtcNow};

                _publisherService.PublishEvent(setDone, Configuration["Events:SetDone:Exchange"], Configuration["Events:SetDone:RoutingKey"]);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
