using ExerciseMS_Application.Handlers.QueryHandlers;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
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

namespace ExerciseMS_Tests.Handlers.Query
{
    public class GetExercisesByCategoryHandlerShould
    {
        private GetExercisesByCategoryHandler handler;

        private readonly Mock<IExerciseMapper>_exerciseMapper = new();
        private readonly Mock<IUnitOfWork>_unitOfWork = new();
        private readonly Mock<IValidator<GetExercisesByCategoryQuery>> _validator = new();

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            GetExercisesByCategoryQuery query = new(Guid.NewGuid(), 0, 0);
            CancellationToken token = new();

            //Act

            _unitOfWork.Setup(u => u.Exercises.GetAllExercisesByCategoryAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Exercise>());
            _exerciseMapper.Setup(e => e.MapEntityToDto(It.IsAny<List<Exercise>>())).Returns(new List<DtoExercise>());

            handler = new(_exerciseMapper.Object, _unitOfWork.Object, _validator.Object);
            await handler.Handle(query, token);

            //Assert

            _unitOfWork.Verify(u => u.Exercises.GetAllExercisesByCategoryAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            _exerciseMapper.Verify(e => e.MapEntityToDto(It.IsAny<List<Exercise>>()), Times.Once());
        }
    }
}
