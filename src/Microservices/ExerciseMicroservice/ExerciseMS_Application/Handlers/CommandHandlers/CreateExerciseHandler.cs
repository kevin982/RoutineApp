using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.Services;
using ExerciseMS_Core.UoW;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.CommandHandlers
{
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, DtoExercise>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseMapper _mapper;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IValidator<CreateExerciseCommand> _validator;

        public CreateExerciseHandler(IUnitOfWork unitOfWork, IExerciseMapper mapper, IUserService userService, IImageService imageService, IValidator<CreateExerciseCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _imageService = imageService;
            _validator = validator;
        }

        public async Task<DtoExercise> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {

                await _validator.ValidateAndThrowAsync(request);

                var category = await _unitOfWork.Categories.GetByIdAsync(request.CreateExerciseRequest.CategoryId);

                Exercise exercise = _mapper.MapRequestToEntity(request.CreateExerciseRequest);

                string imageUrl = await _imageService.UploadImageAsync(request.CreateExerciseRequest.Image);

                if (string.IsNullOrEmpty(imageUrl)) throw new ExerciseMSException("The image could not be uploaded") { StatusCode = 400 };

                exercise.ImageUrl = imageUrl;

                exercise.UserId = new Guid(_userService.GetUserId());

                exercise.Category = category;

                Exercise result = await _unitOfWork.Exercises.CreateAsync(exercise);

                await _unitOfWork.CompleteAsync();

                return _mapper.MapEntityToDto(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
