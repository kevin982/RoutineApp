using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{
 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            CookieOptions cookieOptions = new();

            cookieOptions.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("Nombre", "Kevin", cookieOptions);

            return View();
        }


        public IActionResult ReadCookie()
        {
            var cookieValue = Request.Cookies["Nombre"];

            return View("Index");
        }
    }
}
