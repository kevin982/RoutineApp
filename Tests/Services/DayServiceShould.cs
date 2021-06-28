using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineApp.Repositores.Interfaces;
using InfrastructureRoutineApp.Services.Classes;
using InfrastructureRoutineApp.Validations.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class DayServiceShould
    {
        private readonly Mock<IDayRepository> _dayRepository = new Mock<IDayRepository>();
        private readonly Mock<IDayServiceValidator> _dayServiceValidator = new Mock<IDayServiceValidator>();

        private DayService dayService;


        #region GetAllDaysAsync

        [Fact]
        public async Task ThrowExceptionIfDaysAreNull()
        {
            //Arrange

            List<Day> list = null;

            _dayRepository.Setup(d => d.GetAllDaysAsync()).ReturnsAsync(list);

            dayService = new DayService(_dayRepository.Object, _dayServiceValidator.Object);

            //Act

            //Assert

            await Assert.ThrowsAsync<Exception>( async () =>
               await dayService.GetAllDaysAsync()
            );

        }

        [Fact]
        public async Task ReturnTheCorrectDays()
        {
            //Arrange

            List<Day> listDays = new()
            {
                new Day { Id = 1, DayName = "Monday", Exercises = null },
                new Day { Id = 2, DayName = "Tuesday", Exercises = null },
                new Day { Id = 3, DayName = "Wednesday", Exercises = null }
            };

            List<Day> listDaysResult;

            _dayRepository.Setup(d => d.GetAllDaysAsync()).ReturnsAsync(listDays);

            dayService = new DayService(_dayRepository.Object, _dayServiceValidator.Object);


            //Act

            listDaysResult = await dayService.GetAllDaysAsync();
 

            //Assert

            Assert.Equal(listDaysResult, listDays);
 
        }

        #endregion


        #region GetDayByIdAsync

        [Fact]
        public async Task ThrowExceptionIfGetDayModelIsNotValid()
        {
            //Arrange

            GetDayRequestModel model = null;

            _dayServiceValidator.Setup(v => v.GetDayByIdModelValidation(model)).Returns((false, "The model to get the day must not be null."));

            dayService = new DayService(_dayRepository.Object, _dayServiceValidator.Object);

            //Act

            //Assert

            await Assert.ThrowsAsync<Exception>(async () => 
            {
                await dayService.GetDayByIdAsync(model);
            });
        }

        
        [Fact]
        public async Task ThrowExceptionIfTheDayIsNull()
        {
            //Arrange

            Day day = null;

            _dayServiceValidator.Setup(v => v.GetDayByIdModelValidation(It.IsAny<GetDayRequestModel>())).Returns((true, "Ok"));

            _dayRepository.Setup(d => d.GetDayByIdAsync(It.IsAny<GetDayRequestModel>())).ReturnsAsync(day);

            dayService = new DayService(_dayRepository.Object, _dayServiceValidator.Object);

            //Act

            //Assert

            await Assert.ThrowsAsync<Exception>(async () =>
                await dayService.GetDayByIdAsync(null)
            );

            _dayServiceValidator.Verify(v => v.GetDayByIdModelValidation(It.IsAny<GetDayRequestModel>()), Times.Once());
 

            _dayRepository.Verify(d => d.GetDayByIdAsync(It.IsAny<GetDayRequestModel>()), Times.Once());
        }


        #endregion

    }

}
