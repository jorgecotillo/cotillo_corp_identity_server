using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Julio.Francisco.De.Iriarte.IdentityServer.ApiConfiguration
{
    public class Config
    {
        //This method is needed otherwise IdentiyServer4 was throwing an exception
        //related to invalid scope: openid
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email() // <-- usefull
            };
        }
        
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "Angular2 JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris =           { "http://localhost:4040/callback" },
                    //NOTE: This link needs to match the link from the presentation layer - oidc-client
                    //      otherwise IdentityServer won't display the link to go back to the site
                    PostLogoutRedirectUris = { "http://localhost:4040/home" }, 
                    AllowedCorsOrigins =     { "http://localhost:4040" },
                    EnableLocalLogin = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1"
                    }
                }
            };
        }
    }
}