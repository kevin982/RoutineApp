using Microsoft.Extensions.Configuration;
using Moq;
using RoutineMS_Application.Commands;
using RoutineMS_Application.Handlers;
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
    public class SetDoneHandlerShould
    {
        private SetDoneHandler handler { get; set; }

        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IPublisherService> _publisherService = new();
        private readonly Mock<IConfiguration> Configuration = new();

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CallTheCorrectMethods(bool isNull) 
        {
            //Arrange

            Exercise exercise = (isNull) ? new Exercise() :new Exercise() { SetDetail = new()};

            //Act

            _unitOfWork.Setup(u => u.Exercises.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(exercise);
            _unitOfWork.Setup(u => u.SetsDetails.DeleteOldDetailsAsync());
            _unitOfWork.Setup(u => u.SetsDetails.CreateAsync(It.IsAny<SetDetail>())).ReturnsAsync(It.IsAny<SetDetail>());
            _unitOfWork.Setup(u => u.CompleteAsync());
            _publisherService.Setup(p => p.PublishEvent(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()));

            handler = new(_unitOfWork.Object, _publisherService.Object, Configuration.Object);

            await handler.Handle(new SetDoneCommand(new SetDoneRequest()), new CancellationToken());

            //Assert

            _unitOfWork.Verify(u => u.Exercises.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            _unitOfWork.Verify(u => u.SetsDetails.DeleteOldDetailsAsync(), Times.Once());
            if(isNull)_unitOfWork.Verify(u => u.SetsDetails.CreateAsync(It.IsAny<SetDetail>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(),Times.Exactly(2));
            _publisherService.Verify(p => p.PublishEvent(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
 
    }
}
