using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // validates the user credentials and creates the Claimlist of the User.
        // These Claims will be saved in the token
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var appUser = _userRepository.GetUserByEmail(context.UserName);

            if (appUser == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User not found");
                return Task.FromResult(0);
            }else if (appUser.Password !=
                      appUser.CreatePasswordHash(context.Password, Convert.FromBase64String(appUser.SaltString)))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "invalid email or password");
            }
            else
            {
                var claimList = new List<Claim>
                {
                    new Claim(PKClaims.Lastname, appUser.Lastname),
                    new Claim(PKClaims.Firstname, appUser.Firstname),
                    new Claim(PKClaims.Email, appUser.Email),
                    new Claim(PKClaims.RzName, appUser.RzName),
                    new Claim(PKClaims.EmployeeType, appUser.EmployeeType)
                };

                context.Result = new GrantValidationResult(
                    subject: appUser.Id.ToString(),
                    authenticationMethod: "PK Resource Owner Flow (password)",
                    claims: claimList);
            }
            return Task.FromResult(0);
        }
    }
}