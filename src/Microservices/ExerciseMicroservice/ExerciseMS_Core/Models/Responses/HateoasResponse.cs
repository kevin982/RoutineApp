﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Models.Responses
{
    public class HateoasResponse
    {
        public IEnumerable<Link> Links { get; set; }

        public string Title { get; set; }

        public int StatusCode { get; set; }

        public object Content { get; set; }

        public bool Succeeded { get; set; }
        
    }

}
