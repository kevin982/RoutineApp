﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineLibrary.Entities
{
    public class UserWeight
    {
        public int Id { get; set; } = 0;

        public User User { get; set; } = new();

        public string UserId { get; set; } = string.Empty;

        public int Kilos { get; set; } = 0;

        public DateTime Date { get; set; } = new();
    }
}
