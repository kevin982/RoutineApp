using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Core.Repositories;
using RoutineMS_Infraestructure.Data;
using System;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Repositories
{
    public class DayRepository : Repository<Day>, IDayRepository
    {
        public DayRepository(RoutineMsDbContext context) : base(context) {}

        public async Task<Day> GetDayByDayNumberAsync(int dayNumber)
        {
            try
            {
                var day = await _context.Days.FindAsync(dayNumber);

                if (day is not null) return day;

                throw new RoutineMSException($"The day with id {dayNumber} could not be found") { StatusCode = 404 };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
