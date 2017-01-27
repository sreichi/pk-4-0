using System;
using System.Collections.Generic;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Utilclass;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Helper
{
    public class LdapHelper
    {
        public static readonly int LdapPort = LdapConnection.DEFAULT_PORT;
        public static readonly string LdapHost = "ldap1.hs-augsburg.de";

        public static bool ValidateCredentials(string ldapUid, string password)
        {
            var objectDn = $"uid={ldapUid},ou=People,dc=fh-augsburg,dc=de";
            LdapConnection conn;

            try
            {
                conn= new LdapConnection();
                conn.Connect(LdapHost,LdapPort);
                conn.Bind(objectDn,password);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return false;
            }
            conn.Disconnect();
            return true;
        }


        public static Dictionary<string, string> GetLdapUser(string ldapUid)
        {
            var objectDn = $"uid={ldapUid},ou=People,dc=fh-augsburg,dc=de";

            try
            {
                LdapConnection conn= new LdapConnection();
                conn.Connect(LdapHost,LdapPort);
                var lsc = conn.Search("ou=People,dc=fh-augsburg,dc=de",
                    LdapConnection.SCOPE_ONE,
                    $"uid={ldapUid}",
                    null,
                    false);

                var newDict = new Dictionary<string, string>();
                while (lsc.hasMore())
                {
                    LdapEntry nextEntry = null;
                    try
                    {
                        nextEntry = lsc.next();
                    }
                    catch(LdapException e)
                    {
                        Console.WriteLine("Error: " + e.LdapErrorMessage);
                        // Exception is thrown, go for next entry
                        continue;
                    }
                    LdapAttributeSet attributeSet = nextEntry.getAttributeSet();
                    System.Collections.IEnumerator ienum=attributeSet.GetEnumerator();
                    while(ienum.MoveNext())
                    {
                        LdapAttribute attribute=(LdapAttribute)ienum.Current;
                        string attributeName = attribute.Name;
                        string attributeVal = attribute.StringValue;
                        if(!Base64.isLDIFSafe(attributeVal))
                        {
                            byte[] tbyte=SupportClass.ToByteArray(attributeVal);
                            attributeVal=Base64.encode(SupportClass.ToSByteArray(tbyte));
                        }
                        newDict.Add(attributeName, attributeVal);
                    }
                }
                conn.Disconnect();
                return newDict;
            }
            catch(LdapException e)
            {
                Console.WriteLine("Error:" + e.LdapErrorMessage);
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }
    }
}