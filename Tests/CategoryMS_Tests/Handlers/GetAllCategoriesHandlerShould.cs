using CategoryMS_Application.Handlers.QueryHandlers;
using CategoryMS_Application.Queries;
using CategoryMS_Core.Dtos;
using CategoryMS_Core.Exceptions;
using CategoryMS_Core.Models.Entities;
using CategoryMS_Core.UoW;
using CategoryMS_Tests.Data;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CategoryMS_Tests.Handlers
{
    public class GetAllCategoriesHandlerShould
    {
        private GetAllCategoriesHandler handler;

        private Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task ThrowExceptionIfRequestIsNull()
        {
            //Arrange

            GetAllCategoriesQuery query = null;
            CancellationToken token = new();

            //Act

            handler = new(_unitOfWork.Object);

            //Assert

            await Assert.ThrowsAsync<CategoryMSException>(async () => await handler.Handle(query, token));
        }

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange
            GetAllCategoriesQuery query = new();
            CancellationToken cancellationToken = new();
            
            _unitOfWork.Setup(u => u.Categories.GetAllAsync()).ReturnsAsync(new List<Category>());

            //Act

            handler = new(_unitOfWork.Object);
            await handler.Handle(query, cancellationToken);

            //Assert

            _unitOfWork.Verify(u => u.Categories.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllCategories()
        {
            //Arrange
            GetAllCategoriesQuery query = new();
            CancellationToken cancellationToken = new();

            _unitOfWork.Setup(u => u.Categories.GetAllAsync()).ReturnsAsync(new List<Category>());

            var categories = FakeData.FakeCategories() as List<Category>;

            //Act

            handler = new(_unitOfWork.Object);
            
            var result = await handler.Handle(query, cancellationToken) as List<DtoCategory>;

            //Assert

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(categories[i].CategoryId, result[i].CategoryId);
                Assert.Equal(categories[i].CategoryName, result[i].CategoryName);
            }
        }

    }
}
