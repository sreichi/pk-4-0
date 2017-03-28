using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // Validation happens here
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
                var allPermissions = _permissionRepository.GetPermissionsOfUser(DataSeeder.UserId1);

                var claimList = allPermissions.Select(permission => new Claim("permission", permission.Name)).ToList();
                claimList.Add(new Claim(ProfileClaims.Lastname, appUser.Lastname));
                claimList.Add(new Claim(ProfileClaims.Firstname, appUser.Firstname));
                claimList.Add(new Claim(ProfileClaims.Email, appUser.Email));
                claimList.Add(new Claim(ProfileClaims.EmployeeType, appUser.EmployeeType));

                context.Result = new GrantValidationResult(
                    subject: appUser.Id.ToString(),
                    authenticationMethod: "PK Resource Owner Flow (password)",
                    claims: claimList);
            }
            return Task.FromResult(0);
        }
    }
}