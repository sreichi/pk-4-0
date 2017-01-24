using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.Helper;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public IActionResult RegisterNewUser(string username, string password)
        {
            var validCredentials = LdapAuthenticationHelper.CheckCredentials(username, password);
            if (validCredentials)
            {
                return new OkObjectResult("Correct Credentials");
            }
            return new BadRequestObjectResult("Wrong credentials");
        }
    }
}