using Microsoft.AspNetCore.Identity;
using RoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Data.Entities
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; } = 0;

        public DateTime CreatedOn { get; set; } = new();

        public float Height { get; set; } = 0;

        public DateTime BeganToWorkOutOn { get; set; } = new();

        public List<Routine> Routines { get; set; } = new();

        public List<Exercise> Exercises { get; set; } = new();

        public List<Weight> Weights { get; set; } = new();

        
    }
}
