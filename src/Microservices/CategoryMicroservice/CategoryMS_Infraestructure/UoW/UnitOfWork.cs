using CategoryMS_Core.Repositories;
using CategoryMS_Core.UoW;
using CategoryMS_Infraestructure.Data;
using CategoryMS_Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Infraestructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CategoryMsDbContext _dbContext;

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(CategoryMsDbContext dbContext)
        {
            _dbContext = dbContext;

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
                throw;
            }
        }
    }
}
