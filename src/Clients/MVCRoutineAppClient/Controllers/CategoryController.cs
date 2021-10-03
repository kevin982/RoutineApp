using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCRoutineAppClient.Filters;
using MVCRoutineAppClient.Models;
using MVCRoutineAppClient.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Authorize]
    [Controller]
    [AutoValidateAntiforgeryToken]
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

                ViewBag.Result =result;
            }
            catch (Exception)
            {
                JObject error = new();
                error.Add("succeeded", false);
                error.Add("title", "Internal Error");

                ViewBag.Result = error;
            }

            return View();

        }

        [UserAuthorizationFilter]
        [HttpGet("/v1/Categories")]
        public async Task<string> GetAllCategoriesAsync()
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _categoryService.GetAllCategoriesAsync(accessToken);
            }
            catch (Exception)
            {
                var error = new
                {
                    statusCode = 500,
                    title = "Internal error!",
                    succeded = false
                };

                return JsonSerializer.Serialize(error);
            }
            
            
        }
    }
}
