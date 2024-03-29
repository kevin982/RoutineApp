﻿using ExerciseMS_Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_API
{
    public static class GenericLinks
    {

        private static string AppUri = "https://localhost:9003";

        public static IEnumerable<Link> GetExerciseLinks()
        {
            return new List<Link>()
            {
                new Link(){Name = "Create new exercise", Href = $"{AppUri}/api/v1/Exercise", Method = "POST"},
                new Link(){Name = "Delete an existing exercise", Href = $"{AppUri}/api/v1/Exercise/[exerciseId]", Method = "DELETE"},
                new Link(){Name = "Get all exercises by category", Href = $"{AppUri}/api/v1/Exercise/Category/[categoryId]/[index]/[size]", Method = "GET"},
                new Link(){Name = "Get the exercises count by category", Href = $"{AppUri}/api/v1/Exercise/Category/[categoryId]", Method = "GET" },
                new Link(){Name = "Get the exercises name and id by category", Href = $"{AppUri}/api/v1/Exercise/Category/NameAndId/[categoryId]", Method = "GET" }
            };
        }
    }
}
