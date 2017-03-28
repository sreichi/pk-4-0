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
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            List<Claim> claimList = new List<Claim>();
           

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var type in context.Subject.Claims.ToList())
            {
                claimList.Add(new Claim(type.Type, type.Value));
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