using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Repositories;
using ExerciseMS_Infraestructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ExerciseMsDbContext context) : base(context) { }
    }
}
