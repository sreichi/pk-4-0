using Microsoft.AspNetCore.Mvc;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IAuthenticationRepository
    {
        IActionResult RegisterNewUser(string username, string password);
    }
}