using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap.Utilclass;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.Helper;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<UserListDto> GetAllUsers()
        {
            return _applicationDbContext.AppUser.Select(entry => entry.ToListDto());
        }

        public UserDetailDto GetUserById(Guid userId)
        {
            return _applicationDbContext.AppUser
                .Select(entry => entry.ToDetailDto())
                .SingleOrDefault(e => e.Id == userId);
        }

        public AppUser GetUserByEmail(string email)
        {
            return _applicationDbContext.AppUser.SingleOrDefault(e => e.Email == email);
        }

        public IActionResult CreateUser(string rzName, string rzPassword, UserCreateDto user)
        {
            var rzNameData = Convert.FromBase64String(rzName);
            var decodedRzname = Encoding.UTF8.GetString(rzNameData);

            var rzPasswordData = Convert.FromBase64String(rzPassword);
            var decodedPassword = Encoding.UTF8.GetString(rzPasswordData);

            var ldapCredentialsAreValid = CheckIfLdapCredentialsAreValid(decodedRzname, decodedPassword);
            if (!ldapCredentialsAreValid)
            {
                return new BadRequestObjectResult("Wrong Ldap Credentials");
            }

            var ldapUserAsDictionary = LdapHelper.GetLdapUser(decodedRzname);
            var ldapIdOfUser = Convert.ToInt32(ldapUserAsDictionary["uidNumber"]);

            var userAllreadyExists = CheckIfUserAllreadyExists(ldapIdOfUser, decodedRzname);

            if (userAllreadyExists)
            {
                return new BadRequestObjectResult("User is allready registered");
            }

            var newUser = user.ToModel();

            newUser.EmployeeType = ldapUserAsDictionary["employeeType"];
            newUser.RzName = ldapUserAsDictionary["uid"];
            newUser.LdapId = Convert.ToInt32(ldapUserAsDictionary["uidNumber"]);

            _applicationDbContext.AppUser.Add(newUser);
            Save();

            return new CreatedResult($"/user/{newUser.Id}", newUser.ToDetailDto());
        }

        public IActionResult RemoveRoleFromUser(Guid userId, Guid roleId)
        {
            var userHasRole = _applicationDbContext.UserHasRole.SingleOrDefault(
                entry => entry.UserId == userId && entry.RoleId == roleId);

            if (userHasRole == null)
            {
                return new NotFoundObjectResult("User doesn't have given role");
            }

            _applicationDbContext.UserHasRole.Remove(userHasRole);

            return new OkObjectResult("Role successfully removed from user");
        }

        public IActionResult AssignUserToApplication(Guid userId, RoleDto roleDto)
        {
            var userHasRole = _applicationDbContext.UserHasRole.SingleOrDefault(
                entry => entry.RoleId == roleDto.Id && entry.UserId == userId);

            if (userHasRole != null)
            {
                return new BadRequestObjectResult("User allready has that role");
            }

            _applicationDbContext.UserHasRole.Add(new UserHasRole()
            {
                UserId = userId,
                RoleId = roleDto.Id
            });

            Save();

            var updatedUser = GetUserById(userId);

            return new OkObjectResult(updatedUser);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        private bool CheckIfUserAllreadyExists(int ldapId, string rzName)
        {
            var userToCreate = _applicationDbContext.AppUser.SingleOrDefault(user => user.LdapId == ldapId && user.RzName == rzName);
            return userToCreate != null;
        }

        private bool CheckIfLdapCredentialsAreValid(string rzName, string rzPassword)
        {
            return LdapHelper.ValidateCredentials(rzName, rzPassword);
        }
    }
}