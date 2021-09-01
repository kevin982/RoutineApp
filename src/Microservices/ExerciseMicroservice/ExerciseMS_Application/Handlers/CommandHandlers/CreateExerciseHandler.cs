using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.Services;
using ExerciseMS_Core.UoW;
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
        private readonly IConfiguration Configuration;

        public CreateExerciseHandler(IUnitOfWork unitOfWork, IExerciseMapper mapper, IUserService userService, IImageService imageService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _imageService = imageService;
            Configuration = configuration;
        }

        public async Task<DtoExercise> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CreateExerciseRequest.CategoryId);

            if (category is null) throw new ExerciseMSException("The category has not been found") { StatusCode = 404};

            Exercise exercise = _mapper.MapRequestToEntity(request.CreateExerciseRequest);

            if (exercise == null) throw new ExerciseMSException("The create exercise request can not be null") { StatusCode = 500};

            string imageUrl = await SendImage(request.CreateExerciseRequest.Image);

            if (string.IsNullOrEmpty(imageUrl)) throw new ExerciseMSException("The image could not be uploaded") { StatusCode = 400 };

            exercise.ImageUrl = imageUrl;

            exercise.UserId = new Guid(_userService.GetUserId());

            exercise.Category = category;

            Exercise result = await _unitOfWork.Exercises.CreateAsync(exercise);

            await _unitOfWork.CompleteAsync();

            return _mapper.MapEntityToDto(result);
        }

        private async Task<string> SendImage(IFormFile Image)
        {
            return await _imageService.UploadImageAsync(new UploadImageRequest 
            {
                Image = Image.OpenReadStream(),
                ApiKey =Configuration["Cloudinary:apikey"],
                Cloud =Configuration["Cloudinary:cloud"],
                Secret =Configuration["Cloudinary:secret"],
                ImageName = Image.FileName
            });
        }
    }
}
