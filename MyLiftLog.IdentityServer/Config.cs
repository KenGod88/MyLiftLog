using Duende.IdentityServer.Models;

namespace MyLiftLog.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                {
                new ApiScope(name: "MyLiftLog.Api", displayName: "MyLiftLog API - Full Acces")
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    new Client
                {
                    ClientId = "MyLiftLog.Postman.Client",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "MyLiftLog.Api" }
                 }
                };
    }
}