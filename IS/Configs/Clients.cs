using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IS
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                        ClientId = "Site",
                        ClientName = "Web site",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = new List<Secret>
                        {
                            new Secret("Password".Sha256())
                        },
                        AllowedScopes = new List<string>
                        {
                            "API"
                        }
                },

                new Client
                {
                    ClientId = "js",
                    ClientName = "Портал агрегации (витрина)",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    //AccessTokenLifetime = 330,
                    //AlwaysSendClientClaims = true,
                    RedirectUris = { "https://localhost:44345/callback", "https://localhost:44345/silentRenew"},                    
                    PostLogoutRedirectUris = { "https://localhost:44345/logoutCallback" },
                    AllowedCorsOrigins =  { "https://localhost:44345/" },
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "API",
                        "IntegrationService"
                    }
                }
            };
        }
    }
}
