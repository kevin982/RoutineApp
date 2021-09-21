using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MVCRoutineAppClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IHttpClientFactory httpClientFactory, ILogger<CategoryService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<string> GetAllCategoriesAsync(string accessToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var result = await client.GetAsync("/v1/Category");

                string bodyContent = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(bodyContent)) return bodyContent;

                var errorResponse = new
                {
                    statusCode = (int)result.StatusCode,
                    title = result.ReasonPhrase,
                    succeeded = false
                };

                return JsonConvert.SerializeObject(errorResponse);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<JObject> CreateCategoryAsync(CreateCategoryRequestModel model, string accessToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("/v1/Category", content);

                string bodyContent = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(bodyContent)) return JObject.Parse(bodyContent);

                JObject error = new();

                error.Add("statusCode",(int)result.StatusCode);
                error.Add("title",result.ReasonPhrase);
                error.Add("succeeded",false);

                return error;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
