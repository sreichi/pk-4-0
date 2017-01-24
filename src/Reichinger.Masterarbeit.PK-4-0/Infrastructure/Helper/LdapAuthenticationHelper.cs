using System;
using Novell.Directory.Ldap;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Helper
{
    public class LdapAuthenticationHelper
    {
        public static bool CheckCredentials(string ldapUid, string password)
        {
            var ldapPort = LdapConnection.DEFAULT_PORT;
            var ldapHost = "ldap1.hs-augsburg.de";
            var objectDN = $"uid={ldapUid},ou=People,dc=fh-augsburg,dc=de";

            var conn = new LdapConnection();

            try
            {
                conn= new LdapConnection();
                conn.Connect(ldapHost,ldapPort);
                conn.Bind(objectDN,"password");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return false;
            }
            conn.Disconnect();
            return true;
        }
    }
}