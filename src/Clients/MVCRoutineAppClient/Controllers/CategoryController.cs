using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVCRoutineAppClient.Filters;
using MVCRoutineAppClient.Models;
using MVCRoutineAppClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Controller]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AdminAuthorizationFilter]
        [HttpGet("/v1/Category")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [AdminAuthorizationFilter]
        [HttpPost("/v1/Category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestModel model)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                var result = await _categoryService.CreateCategoryAsync(model, accessToken);

                ViewBag.Succeeded = result.Item1;
                ViewBag.Message = result.Item2;
            }
            catch (Exception ex)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = ex.Message;
            }

            return View();

        }
    }
}
