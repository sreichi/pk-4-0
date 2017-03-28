using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class ProfileClaims
    {
        public static string Lastname = "lastname";
        public static string Firstname = "first_name";
        public static string Email = "email";
        public static string RzName = "rz_name";
        public static string EmployeeType = "employee_type";
        public static string Permissions = "permissions";


        public static List<string> ClaimList()
        {
            return new List<string>
            {
                Lastname,
                Firstname,
                Email,
                RzName,
                EmployeeType,
                Permissions
            };
        }
    }
}