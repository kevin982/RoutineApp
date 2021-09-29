using Microsoft.Extensions.Configuration;
using Moq;
using RoutineMS_Application.Commands;
using RoutineMS_Application.Handlers;
using RoutineMS_Core.Exceptions;
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
    public class RemoveExerciseFromRoutineHandlerShould
    {
        private RemoveExerciseFromRoutineHandler handler { get; set; }

        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IPublisherService> _publisherService = new();
        private readonly Mock<IUserService> _userService = new();
        private readonly Mock<IConfiguration> Configuration = new();

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange
            _unitOfWork.Setup(u => u.Routines.TheExerciseIsInRoutineAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);
            _unitOfWork.Setup(u => u.Routines.RemoveRoutineByExerciseAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);
            _unitOfWork.Setup(u => u.CompleteAsync());
            _userService.Setup(u => u.GetUserId()).Returns(Guid.NewGuid().ToString());
            _publisherService.Setup(p => p.PublishEvent(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()));


            //Act

            handler = new(_unitOfWork.Object, _publisherService.Object, _userService.Object, Configuration.Object);

            await handler.Handle(new RemoveExerciseFromRoutineCommand(Guid.NewGuid()), new CancellationToken());

            //Assert

            _unitOfWork.Verify(u => u.Routines.TheExerciseIsInRoutineAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _unitOfWork.Verify(u => u.Routines.RemoveRoutineByExerciseAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
            _userService.Verify(u => u.GetUserId(), Times.Once());
            _publisherService.Verify(p => p.PublishEvent(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task ThrowExceptionIfExerciseIsNotInRoutine()
        {
            //Arrange

            //Act

            _userService.Setup(u => u.GetUserId()).Returns(Guid.NewGuid().ToString());
            _unitOfWork.Setup(u => u.Routines.TheExerciseIsInRoutineAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(false);
            handler = new(_unitOfWork.Object, _publisherService.Object, _userService.Object, Configuration.Object);

            //Assert

            await Assert.ThrowsAsync<RoutineMSException>(async () => await handler.Handle(new RemoveExerciseFromRoutineCommand(Guid.NewGuid()), new CancellationToken()));
        }
    }
}
