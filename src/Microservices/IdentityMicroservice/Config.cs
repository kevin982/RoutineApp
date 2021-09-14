// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityMicroservice
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
         new List<IdentityResource>
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
         };

        public static IEnumerable<ApiScope> ApiScopes =>
       new List<ApiScope>
       {
            new ApiScope("exerciseMs.all", "All crud operations for the catalog exercise micro service"),
            new ApiScope("routineMs.all", "All crud operations for the routine micro service"),
            new ApiScope("statistisMs.all", "All crud operations for the statistics micro service"),
            new ApiScope("role", "The user roles", new List<string>{ "role"})
       };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
            };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "RoutineMVC",

                ClientSecrets = { new Secret("RoutineMVCSecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                
                RequirePkce = true,

                // where to redirect to after login
                RedirectUris = { "https://localhost:9000/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:9000/signout-callback-oidc" },

                AllowOfflineAccess = true,


                // scopes that client has access to
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "role",
                    "exerciseMs.all",
                    "routineMs.all",
                    "statistisMs.all"
                }
            }
        };
    }
}