﻿using ExerciseMS_Core.Services;
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

        public string GetUserId() => _httpContext.HttpContext.User?.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

        public bool UserIsAuthenticated() => _httpContext.HttpContext.User.Identity.IsAuthenticated;
    }
}