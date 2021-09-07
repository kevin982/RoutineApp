using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Services
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

                if (string.IsNullOrEmpty(id)) throw new ExerciseMSException("The user is not authenticated") { StatusCode = 401 };

                return id;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public bool UserIsAuthenticated() 
        {
            try
            {
                return _httpContext.HttpContext.User.Identity.IsAuthenticated;
            }
            catch (Exception) 
            { 
                throw;
            }

        } 
    }
}
