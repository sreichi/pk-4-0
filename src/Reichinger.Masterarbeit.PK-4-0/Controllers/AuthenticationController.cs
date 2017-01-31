using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class AuthenticationController
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/users/register")]
        public virtual IActionResult CheckUserOnLdap([FromHeader]string username, [FromHeader]string password)
        {
            if (username == null || password == null)
            {
                return new BadRequestObjectResult("Username or Password not set");
            }
            return _authenticationRepository.CheckUserInLdapAndReturnAttributes(username, password);
        }
    }
}