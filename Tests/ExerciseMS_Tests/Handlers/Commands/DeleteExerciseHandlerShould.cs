using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Handlers.CommandHandlers;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.UoW;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExerciseMS_Tests.Handlers.Commands
{
    public class DeleteExerciseHandlerShould
    {
        private DeleteExerciseHandler handler;

        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IValidator<DeleteExerciseCommand>> _validator = new();
 
        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            DeleteExerciseCommand command = new(Guid.NewGuid());
            CancellationToken token = new();


            //Act

            _unitOfWork.Setup(u => u.Exercises.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(new Exercise());

            handler = new(_unitOfWork.Object, _validator.Object);

            await handler.Handle(command, token);

            //Assert

            _unitOfWork.Verify(u => u.Exercises.DeleteAsync(It.IsAny<Guid>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
        }


    }
}
