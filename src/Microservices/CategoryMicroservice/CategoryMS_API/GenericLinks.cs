using CategoryMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_API
{
    public static class GenericLinks
    {

        private static string AppUri = "https://localhost:9004";

        public static IEnumerable<Link> GetCategoryLinks()
        {
            return new List<Link>()
            {
                new Link(){Name = "Create a new category", Href = $"{AppUri}/api/v1/Category", Method = "POST"},
                new Link(){Name = "Get all categories", Href = $"{AppUri}/api/v1/Category", Method = "GET"}
            };
        }
    }
}
