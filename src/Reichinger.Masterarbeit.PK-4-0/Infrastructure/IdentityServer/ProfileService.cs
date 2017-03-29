using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class ProfileService : IProfileService
    {
        // returns the Profile Data for a user
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            List<Claim> claimList = new List<Claim>();


            foreach (var type in PKClaims.ClaimList())
            {
                var claim = context.Subject.Claims.ToList().Find(s => s.Type == type);
                if (claim != null && !string.IsNullOrEmpty(claim.Value))
                {
                    claimList.Add(new Claim(type, claim.Value));
                }
            }

            context.IssuedClaims = claimList;

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}