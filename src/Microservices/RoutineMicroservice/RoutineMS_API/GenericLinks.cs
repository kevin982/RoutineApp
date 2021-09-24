using RoutineMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_API
{
    public static class GenericLinks
    {
        private static string AppUri { get; set; } = "https://localhost:9005";
        public static IEnumerable<Link> GetRoutineLinks()
        {
            return new List<Link>()
            {
                new Link(){Name = "Add an exercise to the routine", Href = $"{AppUri}/api/v1/Routine", Method = "POST"},
                new Link(){Name = "Remove an exercise from routine", Href = $"{AppUri}/api/v1/Routine/[exerciseId]", Method = "DELETE"},
                new Link(){Name = "Add a new set done", Href = $"{AppUri}/api/v1/Routine/SetDone", Method = "GET"},
                new Link(){Name = "Get the exercise to do", Href = $"{AppUri}/api/v1/Routine", Method = "GET" }
            };
        }
    }
}
