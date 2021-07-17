 
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineLibrary;
using DomainRoutineLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class DayRepository : IDayRepository
    {

        private readonly RoutineContext _context = null;

        public DayRepository(RoutineContext context)
        {
            _context = context;
        }

        //This method gets all the days and if the there are not days, then method call the method which seeds the days in the database and then returns them.
        public async Task<List<Day>> GetAllDaysAsync()
        {
            var days = await _context.Days.AsNoTracking().ToListAsync();

            if (days.Count() != 0 && days is not null) return days;

            await SeedAllDays();

            return await GetAllDaysAsync();
        }
         
        //This method seeds all the days
        private async Task SeedAllDays()
        {
            List<Day> days = new()
            {
                new Day() { Id = 1, DayName = "Monday" },
                new Day() { Id = 2, DayName = "Tuesday" },
                new Day() { Id = 3, DayName = "Wednesday" },
                new Day() { Id = 4, DayName = "Thursday" },
                new Day() { Id = 5, DayName = "Friday" },
                new Day() { Id = 6, DayName = "Saturday" },
                new Day() { Id = 7, DayName = "Sunday" }
            };

            days.ForEach(d => _context.Days.Add(d));    

            await _context.SaveChangesAsync();
        }


        //This method get the day according to its id. It there are not days then the method calls the method which seeds the days.
        public async Task<Day> GetDayByIdAsync(GetDayRequestModel model)
        {
            if (model.DayId < 1 || model.DayId > 7) throw new ArgumentOutOfRangeException("The day id must be between 1 and 7.");


            var day =  await _context.Days
                .Where(d => d.Id == model.DayId)
                .FirstOrDefaultAsync();

            if(day is null) await SeedAllDays();

            return await _context.Days
                .Where(d => d.Id == model.DayId)
                .FirstOrDefaultAsync();
        }

        //This method returns the day id
        public async Task<int> GetDayIdAsync()
        {
            string day = DateTime.Now.DayOfWeek.ToString();

            var result = await _context.Days
                .AsNoTracking()
                .Where(d => d.DayName == day)
                .Select(d => new { d.Id })
                .SingleAsync();

            if(result is not null) return result.Id;

            await SeedAllDays();

            result = await _context.Days
               .AsNoTracking()
               .Where(d => d.DayName == day)
               .Select(d => new { d.Id })
               .SingleAsync();

            return result.Id;
        }
    }
}
