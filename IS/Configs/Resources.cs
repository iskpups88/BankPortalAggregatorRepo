using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace IS
{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources(IConfiguration Configuration)
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "API",
                    DisplayName = "Портал",
                    Description = "Портал агрегации финансовых услуг",
                    ApiSecrets = new List<Secret> { new Secret(Configuration["Authentication:SecretKeyAPI"].Sha256()) },
                    Scopes = new List<Scope>
                    {
                        new Scope("API", "Портал Агрегации"),
                    }
                },
                new ApiResource
                {
                    Name = "Integration",
                    DisplayName = "Ак Барс",
                    Description = "Ак Барс Банк интеграционный сервис",
                    ApiSecrets = new List<Secret> { new Secret(Configuration["Authentication:SecretKeyIntegtationService"].Sha256()) },

                    Scopes = new List<Scope>
                    {
                        new Scope("IntegrationService", "Ак Барс Банк")
                    }
                }
            };
        }
    }
}
