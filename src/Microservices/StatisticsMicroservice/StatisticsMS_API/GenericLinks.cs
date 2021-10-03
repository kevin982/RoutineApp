using StatisticsMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_API
{
    public static class GenericLinks
    {
        private static string AppUri { get; set; } = "https://localhost:9006";
        public static IEnumerable<Link> GetStatisticsLinks()
        {
            return new List<Link>()
            {
                new Link(){Name = "Get all the exercise statistics", Href = $"{AppUri}/api/v1/Statistics/exerciseId/0/0", Method = "GET"},
                new Link(){Name = "Get all the exercises statistics in August 2021", Href = $"{AppUri}/api/v1/Statistics/exerciseId/8/2021", Method = "GET" }
            };
        }
    }
}
