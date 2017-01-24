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

        [HttpGet]
        [Route("/users/register")]
        public virtual IActionResult CheckUserOnLdap([FromHeader]long? token, [FromBody]string username, [FromBody] string password)
        {
            return _authenticationRepository.RegisterNewUser(username, password);
        }
    }
}