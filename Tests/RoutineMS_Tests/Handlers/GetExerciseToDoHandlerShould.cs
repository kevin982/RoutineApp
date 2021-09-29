using Moq;
using RoutineMS_Application.Handlers;
using RoutineMS_Application.Queries;
using RoutineMS_Core.Dtos;
using RoutineMS_Core.Services;
using RoutineMS_Core.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RoutineMS_Tests.Handlers
{
    public class GetExerciseToDoHandlerShould
    {
        private GetExerciseToDoHandler handler { get; set; }

        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IUserService> _userService = new();

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            _unitOfWork.Setup(u => u.SetsDetails.DeleteOldDetailsAsync());
            _unitOfWork.Setup(u => u.CompleteAsync());
            _userService.Setup(u => u.GetUserId()).Returns("2c544a75-48df-4fba-9b3c-ccba82b4731e");
            _unitOfWork.Setup(u => u.Routines.GetExerciseToDoFromRoutineAsync(It.IsAny<Guid>(), It.IsAny<int>())).ReturnsAsync(It.IsAny<ExerciseToDoDto>());

            //Act

            handler = new(_unitOfWork.Object, _userService.Object);

            await handler.Handle(new GetExerciseToDoQuery(), new CancellationToken());

            //Assert

            _unitOfWork.Verify(u => u.SetsDetails.DeleteOldDetailsAsync(), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
            _userService.Verify(u => u.GetUserId(), Times.Once());
        }
    }
}
