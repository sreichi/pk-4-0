using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class PKClaims
    {
        public static string Lastname = "name";
        public static string Firstname = "first_name";
        public static string Email = "email";


        public static List<string> ClaimList()
        {
            return new List<string>
            {
                Lastname,
                Firstname,
                Email
            };
        }
    }
}