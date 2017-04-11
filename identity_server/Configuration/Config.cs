using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace Julio.Francisco.De.Iriarte.IdentityServer.Configuration
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

        public static IEnumerable<Client> GetClients(IConfigurationRoot configuration)
        {
            var jsDomain = configuration["general:JSDomain"];
            var wpDomain = configuration["general:WordpressDomain"];

            return new List<Client>
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "Angular2 JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris =           { jsDomain + "/callback" },
                    //NOTE: This link needs to match the link from the presentation layer - oidc-client
                    //      otherwise IdentityServer won't display the link to go back to the site
                    PostLogoutRedirectUris = { jsDomain + "/home" }, 
                    AllowedCorsOrigins =     { jsDomain },
                    EnableLocalLogin = false,
					AlwaysSendClientClaims = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1"
                    }
                },
                new Client
                {
                    ClientId = "wordpress",
                    ClientName = "Wordpress Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris =           { wpDomain + "/wp-admin/admin-ajax.php?action=openid-connect-authorize" },
                        //NOTE: This link needs to match the link from the presentation layer - oidc-client
                        //      otherwise IdentityServer won't display the link to go back to the site
                        PostLogoutRedirectUris = { wpDomain },
                    AllowedCorsOrigins =     { wpDomain },
                    EnableLocalLogin = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },
                    //AlwaysIncludeUserClaimsInIdToken = true,
                            //AlwaysSendClientClaims = true,
                    ClientSecrets = new List<Secret>() { new Secret("VUdPR5HIlKLe4sVmMe6JbZk8v/JMZC5qy8VY2Chdfrg=".Sha256()) }
                }
            };
        }
    }
}