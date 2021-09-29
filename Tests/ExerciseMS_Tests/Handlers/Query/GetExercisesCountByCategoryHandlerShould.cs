using ExerciseMS_Application.Handlers.QueryHandlers;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
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

namespace ExerciseMS_Tests.Handlers.Query
{
    public class GetExercisesCountByCategoryHandlerShould
    {
        GetIndexesCountHandler hanlder;

        private readonly Mock<IUnitOfWork>_unitOfWork = new();
        private readonly Mock<IValidator<GetIndexesCount>> _validator = new();

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            GetIndexesCount query = new(Guid.NewGuid(), 5);
            CancellationToken token = new();

            //Act

            _unitOfWork.Setup(u => u.Exercises.GetIndexesCount(It.IsAny<Guid>(), It.IsAny<int>())).Returns(5);

            hanlder = new(_unitOfWork.Object, _validator.Object);

            await hanlder.Handle(query, token);

            //Assert

            _unitOfWork.Verify(u => u.Exercises.GetIndexesCount(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once());
        }
    }
}
