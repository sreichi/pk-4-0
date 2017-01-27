using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ApplicationDbContext _dbContext;

        public ResourceOwnerPasswordValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // Validation happens here
            var appUser = _dbContext.AppUser.SingleOrDefault(user => user.Email == context.UserName && user.Password == context.Password);

            if (appUser == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Username or password is incorrect");
                return Task.FromResult(0);
            }

            context.Result = new GrantValidationResult(appUser.Id.ToString(), "password");
            return Task.FromResult(0);
        }
    }
}