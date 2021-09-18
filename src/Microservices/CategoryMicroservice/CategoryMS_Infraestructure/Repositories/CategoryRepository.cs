using CategoryMS_Core.Models.Entities;
using CategoryMS_Core.Repositories;
using CategoryMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Infraestructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CategoryMsDbContext context) : base(context) { }
    }
}
