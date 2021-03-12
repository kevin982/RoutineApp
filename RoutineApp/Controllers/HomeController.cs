using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoutineApp.Data;
using RoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly RoutineContext c = null;

        public HomeController(RoutineContext cc)
        {
            c = cc;
        }
        public IActionResult Index()
        { 
            return View();
        }
    }
}
