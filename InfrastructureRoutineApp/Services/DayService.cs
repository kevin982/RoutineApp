using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository = null;
        
        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }


        public async Task<List<Day>> GetAllDaysAsync()
        {
            return await _dayRepository.GetAllDaysAsync();
        }

        public async Task<Day> GetDayByIdAsync(GetDayRequestModel model)
        {
            return await _dayRepository.GetDayByIdAsync(model);
        }

        public async Task<int> GetDayIdAsync()
        {
            return await _dayRepository.GetDayIdAsync();
        }
    }
}
