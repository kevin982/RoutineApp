 
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using DomainRoutineLibrary.Entities;
using InfrastructureRoutineApp.Validations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services.Classes
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository = null;
        private readonly IDayServiceValidator _dayServiceValidator= null;

        public DayService(IDayRepository dayRepository, IDayServiceValidator dayServiceValidator)
        {
            _dayRepository = dayRepository;
            _dayServiceValidator = dayServiceValidator;
        }


        public async Task<List<Day>> GetAllDaysAsync()
        {
            var days = await _dayRepository.GetAllDaysAsync();

            if (days is null) throw new Exception("There are no days registered.");

            return days;
        }

        public async Task<Day> GetDayByIdAsync(GetDayRequestModel model)
        {
            var resultValidation = _dayServiceValidator.GetDayByIdModelValidation(model);

            if (!resultValidation.Valid) throw new Exception(resultValidation.Message);

            var day  = await _dayRepository.GetDayByIdAsync(model);


            if (day is null) throw new Exception("We could not get the day by its id.");

            return day;
        }

        public async Task<int> GetDayIdAsync()
        {
            var day = await _dayRepository.GetDayIdAsync();

            if (day < 1 || day > 7) throw new Exception("We could not get the day number.");

            return day;
        }
    }
}
