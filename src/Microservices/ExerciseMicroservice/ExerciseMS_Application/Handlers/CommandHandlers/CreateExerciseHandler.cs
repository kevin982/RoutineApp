using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
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
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, bool>
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

        public async Task<bool> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CreateExerciseRequest.CategoryId);

            Exercise exercise = _mapper.MapRequestToEntity(request.CreateExerciseRequest);

            if (exercise == null) return false;

            string imageUrl = await SendImage(request.CreateExerciseRequest.Image);

            if (string.IsNullOrEmpty(imageUrl)) return false;

            exercise.ImageUrl = imageUrl;

            exercise.UserId = new Guid(_userService.GetUserId());

            exercise.Category = category;

            bool result = await _unitOfWork.Exercises.CreateAsync(exercise);

            if (result) await _unitOfWork.CompleteAsync();

            return result;
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
