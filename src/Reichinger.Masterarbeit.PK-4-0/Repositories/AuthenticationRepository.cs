using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.Helper;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public IActionResult CheckUserInLdapAndReturnAttributes(string username, string password)
        {
            var validCredentials = LdapHelper.ValidateCredentials(username, password);
            if (!validCredentials) return new BadRequestObjectResult("Wrong credentials");
            var ldapUserAttributes = LdapHelper.GetLdapUser(username);
            if (ldapUserAttributes == null) return new NotFoundObjectResult("User Not Found in LDAP");
            return new OkObjectResult(ldapUserAttributes);
        }
    }
}