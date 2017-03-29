using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    // defines all Claims a user contains in the token
    public class PKClaims
    {
        public static string Lastname = "lastname";
        public static string Firstname = "first_name";
        public static string Email = "email";
        public static string RzName = "rz_name";
        public static string EmployeeType = "employee_type";


        public static List<string> ClaimList()
        {
            return new List<string>
            {
                Lastname,
                Firstname,
                Email,
                RzName,
                EmployeeType
            };
        }
    }
}