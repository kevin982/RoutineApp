using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;
using RoutineMS_Application.Commands;
using RoutineMS_Application.Handlers;
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
using Xunit;

namespace RoutineMS_Tests.Handlers
{
    public class AddExerciseToRoutineHandlerShould
    {
        private AddExerciseToRoutineHanlder handler { get; set; }

        private Mock<IUnitOfWork> _unitOfWork = new();
        private Mock<IValidator<AddExerciseToRoutineCommand>> _validator = new ();
        private Mock<IUserService> _userService = new ();
        private Mock<IPublisherService> _publisherService = new ();
        private Mock<IConfiguration> Configuration = new();


        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            AddExerciseToRoutineCommand command = new(new AddExerciseToRoutineRequest() { CategoryName = "Back", Days = new List<int> { 1, 3}, ExerciseName = "Chin up", Sets = 5, ExerciseId = new Guid("fb4b2d04-f1d9-4d8f-92b1-f7755a7d7899"), ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" });
            CancellationToken token = new();

            //Act

            _unitOfWork.Setup(u => u.Routines.TheExerciseIsInRoutineAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(false);
            _unitOfWork.Setup(u => u.Exercises.CreateAsync(It.IsAny<Exercise>())).ReturnsAsync(It.IsAny<Exercise>());
            _unitOfWork.Setup(u => u.Routines.CreateAsync(It.IsAny<Routine>())).ReturnsAsync(It.IsAny<Routine>());
            _unitOfWork.Setup(u => u.Days.GetDayByDayNumberAsync(It.IsAny<int>())).ReturnsAsync(It.IsAny<Day>());
            _userService.Setup(u => u.GetUserId()).Returns("fb4b2d04-f1d9-4d8f-92b1-f7755a7d7899");
            _unitOfWork.Setup(u => u.CompleteAsync());

            _publisherService.Setup(p => p.PublishEvent(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()));

            handler = new(_unitOfWork.Object, _validator.Object, _userService.Object, _publisherService.Object, Configuration.Object);

            await handler.Handle(command, token);
            //Assert

            _unitOfWork.Verify(u => u.Routines.TheExerciseIsInRoutineAsync(It.IsAny<Guid>(), It.IsAny<Guid>()),Times.Once());
            _unitOfWork.Verify(u => u.Exercises.CreateAsync(It.IsAny<Exercise>()), Times.Once());
            _unitOfWork.Verify(u => u.Routines.CreateAsync(It.IsAny<Routine>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
            _unitOfWork.Verify(u => u.Days.GetDayByDayNumberAsync(It.IsAny<int>()), Times.Exactly(command.Request.Days.Count()));
            _userService.Verify(u => u.GetUserId(), Times.Once());
            _publisherService.Verify(p => p.PublishEvent(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task ThrowExceptionIfExerciseIsAlreadyInRoutine()
        {
            //Arrange
            AddExerciseToRoutineCommand command = new(new AddExerciseToRoutineRequest() { CategoryName = "Back", Days = new List<int> { 1, 3 }, ExerciseName = "Chin up", Sets = 5, ExerciseId = new Guid("fb4b2d04-f1d9-4d8f-92b1-f7755a7d7899"), ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" });
            CancellationToken token = new();

            //Act
            _userService.Setup(u => u.GetUserId()).Returns(Guid.NewGuid().ToString());
            _unitOfWork.Setup(u => u.Routines.TheExerciseIsInRoutineAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);

            handler = new(_unitOfWork.Object, _validator.Object, _userService.Object, _publisherService.Object, Configuration.Object);

            //Assert

            await Assert.ThrowsAsync<RoutineMSException>(async () => await handler.Handle(command, token));
        }

 
    }
}
