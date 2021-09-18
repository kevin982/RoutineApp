using ExerciseMS_Application.Handlers.CommandHandlers;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Core.Services;
using ExerciseMS_Core.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExerciseMS_Application.Commands;
using ExerciseMS_Core.Models.Requests;
using System.Threading;
using Moq;
using ExerciseMS_Core.Models.Entities;
using Microsoft.Extensions.Configuration;
using Xunit;
using ExerciseMS_Core.Exceptions;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace ExerciseMS_Tests.Handlers.Commands
{
    public class CreateExerciseHandlerShould
    {
        private CreateExerciseHandler handler { get; set; }

        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IExerciseMapper> _mapper = new();
        private readonly Mock<IUserService> _userService = new();
        private readonly Mock<IImageService> _imageService = new();
        private readonly Mock<IValidator<CreateExerciseCommand>> _validator = new();
        

 

        [Fact]
        public async Task ThrowExceptionIfImageCouldNotBeLoaded()
        {
            //Arrange
            
            CreateExerciseCommand command = new(new CreateExerciseRequest());
            CancellationToken cancellationToken = new();

            //Act

            _mapper.Setup(u => u.MapRequestToEntity(It.IsAny<CreateExerciseRequest>())).Returns(new Exercise());
            _imageService.Setup(i => i.UploadImageAsync(It.IsAny<IFormFile>())).ReturnsAsync(string.Empty);

            handler = new(_unitOfWork.Object, _mapper.Object, _userService.Object, _imageService.Object, _validator.Object);

            //Assert
            await Assert.ThrowsAsync<ExerciseMSException>(async () => await handler.Handle(command, cancellationToken));
        }

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            CreateExerciseCommand command = new(new CreateExerciseRequest());
            CancellationToken cancellationToken = new();

            //Act

            _mapper.Setup(u => u.MapRequestToEntity(It.IsAny<CreateExerciseRequest>())).Returns(new Exercise());
            _imageService.Setup(i => i.UploadImageAsync(It.IsAny<IFormFile>())).ReturnsAsync("h");
            _userService.Setup(u => u.GetUserId()).Returns(Guid.NewGuid().ToString());
            _unitOfWork.Setup(u => u.Exercises.CreateAsync(It.IsAny<Exercise>())).ReturnsAsync(new Exercise());
           

            handler = new(_unitOfWork.Object, _mapper.Object, _userService.Object, _imageService.Object, _validator.Object);

            await handler.Handle(command, cancellationToken);

            //Assert

            _mapper.Verify(u => u.MapRequestToEntity(It.IsAny<CreateExerciseRequest>()), Times.Once());
            _imageService.Verify(i => i.UploadImageAsync(It.IsAny<IFormFile>()), Times.Once());
            _userService.Verify(u => u.GetUserId(), Times.Once());
            _unitOfWork.Verify(u => u.Exercises.CreateAsync(It.IsAny<Exercise>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());

        }

    }
}
