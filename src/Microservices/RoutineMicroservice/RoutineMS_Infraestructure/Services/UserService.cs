using Microsoft.AspNetCore.Http;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Services;
using System;
using System.Linq;
using System.Security.Claims;

namespace RoutineMS_Infraestructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUserId()
        {
            try
            {
                string id = _httpContext.HttpContext.User?.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

                if (string.IsNullOrEmpty(id)) throw new RoutineMSException("The user is not authenticated") { StatusCode = 401 };

                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
