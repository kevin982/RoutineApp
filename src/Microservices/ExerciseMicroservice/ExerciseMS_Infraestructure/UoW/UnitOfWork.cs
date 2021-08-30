﻿using ExerciseMS_Core.Repositories;
using ExerciseMS_Core.UoW;
using ExerciseMS_Infraestructure.Data;
using ExerciseMS_Infraestructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExerciseMsDbContext _dbContext;

        private readonly ILogger _logger;

        public IExerciseRepository Exercises { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(ExerciseMsDbContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;

            _logger = logger.CreateLogger("ExerciseMsLogs");

            Exercises = new ExerciseRepository(_dbContext, _logger);
            Categories = new CategoryRepository(_dbContext);
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
                _logger.LogError($"Error while saving changes because of: {ex.Message}");
            }
        }
    }
}
