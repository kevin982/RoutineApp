using RoutineMS_Core.Repositories;
using RoutineMS_Core.UoW;
using RoutineMS_Infraestructure.Data;
using RoutineMS_Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RoutineMsDbContext _dbContext;

        public IRoutineRepository Routines { get; private set; }
        public IExerciseRepository Exercises { get; private set; }
        public ISetDetailRepository SetsDetails { get; private set; }
        public IDayRepository Days { get; private set; }

        public UnitOfWork(RoutineMsDbContext dbContext)
        {
            _dbContext = dbContext;

            Routines = new RoutineRepository(_dbContext);
            Exercises = new ExerciseRepository(_dbContext);
            SetsDetails = new SetDetailRepository(_dbContext);
            Days = new DayRepository(_dbContext);
        }

        public async Task CompleteAsync()
        {
            try
            {
                await _dbContext
                    .SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
