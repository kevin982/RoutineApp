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
        public async Task<(bool, dynamic)> GetAllCategoriesAsync(string accessToken)
        {

            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var result = await client.GetAsync("/v1/Category");

                string bodyContent = await result.Content.ReadAsStringAsync();

                _logger.LogInformation(bodyContent);

                if (result.IsSuccessStatusCode)
                {
                    var response = JObject.Parse(bodyContent);

                    _logger.LogInformation($"Response got at {DateTime.UtcNow} = {response["content"].ToString()}");

                    return (true, response["content"]);
                }
                else
                {
                    if (string.IsNullOrEmpty(bodyContent)) return (false, "Error while getting the categories");

                    var response = JObject.Parse(bodyContent);

                    return (false, response["title"].ToString());
                }

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool, string)> CreateCategoryAsync(CreateCategoryRequestModel model, string accessToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("/v1/Category", content);

                if (result.IsSuccessStatusCode) return (true, "Category created!");

                string bodyContent = await result.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(bodyContent)) return (false, "We could not create the category");

                var response = JObject.Parse(bodyContent);

                return (false, response["title"].ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
