﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RoutineApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoutineApp.Claims
{
    public class ApplicationUserClaimsPrincipalFactory:UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FirstName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("Height", user.Height.ToString()));
            identity.AddClaim(new Claim("Age", user.Age.ToString()));
            
            return identity;

        }

    }
}
