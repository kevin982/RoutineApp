using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Controller]
    public class RoutineController : Controller
    {
        [HttpGet("/v1/CreateRoutine")]
        public async Task<IActionResult> CreateRoutine()
        {
            ViewBag.AccessToken = await HttpContext.GetTokenAsync("access_token");
            return View();
        }
    }
}
