using ExerciseMS_Application.Handlers.QueryHandlers;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.UoW;
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
    public class GetAllCategoriesHandlerShould
    {
        private GetAllCategoriesHandler handler;

        private Mock<ICategoryMapper> _mapper = new();

        private Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task ThrowExceptionIfRequestIsNull()
        {
            //Arrange

            GetAllCategoriesQuery query = null;
            CancellationToken token = new();

            //Act

            handler = new(_mapper.Object, _unitOfWork.Object);

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await handler.Handle(query, token));
        }


    }
}
