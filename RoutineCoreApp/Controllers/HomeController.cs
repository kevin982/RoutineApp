using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{



    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            try
            {
                int edad = 12;
                bool esMenorDeEdad = edad < 18;

                if (esMenorDeEdad)
                {
                    throw new ArgumentOutOfRangeException("edad", edad, "You are too young");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogError(e, "Error");
            }

            _logger.LogInformation("This is information");
            
            _logger.LogWarning("This is warning");
            
            _logger.LogError("This is error");
            
            _logger.LogCritical("This is critical");
            
            _logger.LogDebug("This is debug");




            return View();
        }

    }
}
