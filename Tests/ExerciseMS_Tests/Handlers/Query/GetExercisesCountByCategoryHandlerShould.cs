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
        GetExercisesCountByCategoryHandler hanlder;

        private readonly Mock<IUnitOfWork>_unitOfWork = new();
        private readonly Mock<IValidator<GetExercisesCountByCategoryQuery>> _validator = new();

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            GetExercisesCountByCategoryQuery query = new(Guid.NewGuid());
            CancellationToken token = new();

            //Act

            _unitOfWork.Setup(u => u.Exercises.GetExerciseCountByCategory(It.IsAny<Guid>())).Returns(5);

            hanlder = new(_unitOfWork.Object, _validator.Object);

            await hanlder.Handle(query, token);

            //Assert

            _unitOfWork.Verify(u => u.Exercises.GetExerciseCountByCategory(It.IsAny<Guid>()));
        }
    }
}
