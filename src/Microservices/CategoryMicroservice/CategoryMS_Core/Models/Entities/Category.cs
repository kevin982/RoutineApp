using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Core.Models.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
