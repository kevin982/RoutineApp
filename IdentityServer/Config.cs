// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
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
            new ApiScope("modifyroutineapi", "Modify Routine Api")
       };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "RoutineMVC",

                ClientSecrets = { new Secret("RoutineMVCSecret".Sha256()) },


                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                // scopes that client has access to
                AllowedScopes = new List<string>
                {
                    "modifyroutineapi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };
    }
}