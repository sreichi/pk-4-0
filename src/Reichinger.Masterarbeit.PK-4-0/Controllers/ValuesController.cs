using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novell.Directory.Ldap;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{

    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _log;
        private ApplicationDbContext _applicationDbContext;

        public ValuesController(ILogger<ValuesController> logger, ApplicationDbContext applicationDbContext)
        {
            _log = logger;
            _applicationDbContext = applicationDbContext;
        }
        // GET api/values
        [HttpGet]
        [Route("/api/GetUser")]
        public IActionResult GetUser()
        {
            var role = _applicationDbContext.Role;
//            role.Add(new Role()
//            {
//                Name = "test",
//                Id = 14
//            });
            _applicationDbContext.SaveChanges();
            return Ok(_applicationDbContext.Role.First());
//            return Ok(Connect());
        }

        public List<String> Connect()
        {
            var result = new List<String>(); 
            int LdapPort = LdapConnection.DEFAULT_PORT;
            int searchScope = LdapConnection.SCOPE_SUB;
            int LdapVersion = LdapConnection.Ldap_V3; ;
            bool attributeOnly = true;
            String[] attrs = { LdapConnection.NO_ATTRS };
            String ldapHost = "ldap1.hs-augsburg.de";
            String loginDN = "";
            String password = "";
            String searchBase = "ou=People,dc=fh-augsburg,dc=de";
            String searchFilter = "";
            LdapConnection lc = new LdapConnection();

            try
            {
                // connect to the server
                lc.Connect(ldapHost, LdapPort);
                // bind to the server
                lc.Bind(LdapVersion, loginDN, password);

                LdapSearchResults searchResults =
                    lc.Search(searchBase,      // container to search
                        searchScope,     // search scope
                        searchFilter,    // search filter
                        attrs,           // "1.1" returns entry name only
                        attributeOnly);  // no attributes are returned

                // print out all the objects
                while (searchResults.hasMore())
                {
                    LdapEntry nextEntry = null;
                    try
                    {
                        nextEntry = searchResults.next();
                    }
                    catch (LdapException e)
                    {
                        _log.LogError(e.ToString());

                        // Exception is thrown, go for next entry
                        continue;
                    }
                    result.Add(nextEntry.DN);
                    _log.LogInformation(nextEntry.DN);
                }
                // disconnect with the server
                lc.Disconnect();
            }
            catch (LdapException e)
            {
                _log.LogError(e.ToString());
            }
            catch (Exception e)
            {
                _log.LogError(e.ToString());
            }
            return result;
        }
    }
}
