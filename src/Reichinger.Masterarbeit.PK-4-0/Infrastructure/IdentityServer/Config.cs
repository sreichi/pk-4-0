using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class Config
    {
        // returns all Identity Resources
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        // returns all Api Resources
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "PK 4.0 API")
            };
        }


        // returns all Clients which are authorized to send requests to the application
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "a2pkfrontend",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("frontendsecret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api"
                    }
                },
                new Client
                {
                    ClientId = "testclient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("test".Sha256())
                    },
                    AllowedScopes =
                    {
                        "api"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "Admin",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Testuser Admin")
                    }
                },
                new TestUser()
                {
                    SubjectId = "2",
                    Username = "Testuser",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Testuser Agent")
                    }
                }
            };
        }
    }
}